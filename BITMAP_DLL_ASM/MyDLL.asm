.CODE
; Funkcja ConvertASM
; Odpowiedzialna za przeskalowanie obrazu wejsciowego, oraz konwersjê na odpowiedni styl (Monochromatyczny, RGB565, RGB888)
;
; Jako parametry wejœciowe przyjmuje:
; InputPtr - wskaŸnik na tablice bajtów zdjêcia wejœciowego
; OutputPtr - wskaŸnik na tablice bajtów zdjêcia wyjœciowego
; inputWidth - szerokoœæ wejœciowa (int)
; inputHeight - wysokoœæ wejœciowa (int)
; outputWidth - szerokoœæ wyjœciowa (int)
; outputHeight - wysokoœæ wyjœciowa (int)
; mode - styl (int)
;
; Parametry wyjœciowe:
; funkcja zapisuje wyniki w tablicy bajtów obrazu wyjœciowego (OutputPtr)
;
; Rejestry na których wykonywane s¹ operacje:
; r8, r9, r10, r11, r12, r13, r14, rax, rbx, rcx, rdx, rsi, rdi, eax, ecx, edx, xmm0, xmm1, xmm2

ConvertASM proc
    push rsi        ; Wrzuæ na stos wartoœæ RSi
    push rdi        ; Wrzuæ na stos wartoœæ RDI
    push rbx        ; Wrzuæ na stos wartoœæ RBX

    ;rsi - INPUTPTR
    ;rdi - OUTPUTPTR

    mov r11, r8        ; inputWidth
    mov rcx, r9         ; inputHeight
    
    mov r8, [rsp+64]   ; outputWidth (pi¹ty argument)
    mov r9, [rsp+72]   ; outputHeight (szósty argument)
    mov r10, [rsp+80]    ; mode (siódmy argument)

    ; scaleX = inputWidth / outputWidth
    mov rax, r11                ; Przenieœ inputWidth do rax
    cvtsi2sd xmm0, rax          ; Konwertuj inputWidth (int) do double w xmm0

    mov rax, r8                ; Przenieœ outputWidth do rax
    cvtsi2sd xmm1, rax          ; Konwertuj outputWidth (int) do double w xmm1

    ; scaleX = inputWidth / outputWidth
    divsd xmm0, xmm1            ; xmm0 = xmm0 / xmm1 (inputWidth / outputWidth)

    movsd xmm1, xmm0            ; Zachowaj scaleX

    ; scaleY = inputHeight / outputHeight
    mov rax, rcx                ; Przenieœ inputHeight do rax
    cvtsi2sd xmm0, rax          ; Konwertuj inputHeight (int) do double w xmm0

    mov rax, r9                ; Przenieœ outputHeight do rax
    cvtsi2sd xmm2, rax          ; Konwertuj outputHeight (int) do double w xmm2

    divsd xmm0, xmm2           ; xmm0 = xmm0 / xmm2 (inputHeight / outputHeight)

    movsd xmm2, xmm0            ; Zachowaj scaleY

calculate:
    ; Pêtla przez wiersze obrazu wyjœciowego
    xor r12, r12           ; r12 = 0 (indeks wiersza)
    mov r14, rdi           ; wskaŸnik do obrazu wyjœciowego

outer_loop:
    cmp r12, r9                 ; SprawdŸ, czy osi¹gniêto wysokoœæ obrazu wyjœciowego
    je done                    ; Jeœli tak, zakoñcz pêtlê

    xor r13, r13                ; r13 = 0 (indeks kolumny)
inner_loop:
    cmp r13, r8                 ; SprawdŸ, czy osi¹gniêto szerokoœæ obrazu wyjœciowego
    je next_row                ; Jeœli tak, przejdŸ do nastêpnego wiersza


    ; Oblicz skalowane wspó³rzêdne wejœciowe
    ; xInput = r13 * scaleX
    cvtsi2sd xmm0, r13          ; Konwertuj r13 (Xoutput) na double
    mulsd xmm0, xmm1            ; xmm0 = r13 * scaleX
    cvttsd2si rax, xmm0         ; Konwertuj wynik na int (xInput)

    ; yInput = r12 * scaleY
    cvtsi2sd xmm0, r12          ; Konwertuj r12 (Youtput) na double
    mulsd xmm0, xmm2            ; xmm0 = r12 * scaleY
    cvttsd2si rbx, xmm0         ; Konwertuj wynik na int (yInput)

    ; Oblicz pozycjê w obrazie wejœciowym (zak³adaj¹c 3 bajty na piksel)
    imul rbx, r11          ; rdi = yInput * inputWidth (indeks wiersza)
    add rbx, rax                ; rdi += xInput (dodaj kolumnê)

    imul rbx, 3            ; rdi *= 3 (przesuniêcie bajtowe dla RGB)
    
    cmp r10, 0             ; SprawdŸ czy wybrany tryb to Monochromatyczny
    je mono                ; Skocz do obs³ugi trybu

    cmp r10, 565           ; SprawdŸ czy wybrany tryb to RGB565
    je _565                ; Skocz do obs³ugi trybu
                           ; W przeciwnym razie idŸ dalej (RGB888)

    ; Oblicz pozycjê w obrazie wyjœciowym
    mov rdi, r12                ; rdi = Youtput (indeks wiersza)
    imul rdi, r8                ; rdi = Youtput * outputWidth
    add rdi, r13                ; rdi += Xoutput (indeks kolumny)
    imul rdi, rdi, 3            ; rdi *= 3 (przesuniêcie bajtowe dla RGB)

    ; Pobierz wartoœci RGB z obrazu wejœciowego
    movzx rax, byte ptr [rsi + rbx + 0]       ; rax = wartoœæ R
    movzx rcx, byte ptr [rsi + rbx + 1]   ; rcx = wartoœæ G
    movzx rdx, byte ptr [rsi + rbx + 2]   ; rdx = wartoœæ B

    ; Maluj piksel na odpowiedni kolor
    mov byte ptr [r14 + rdi + 2], al      ; Przenieœ do tablicy pod wskazany adres wartoœæ R
    mov byte ptr [r14 + rdi + 1], cl      ; Przenieœ do tablicy pod wskazany adres wartoœæ G
    mov byte ptr [r14 + rdi + 0], dl      ; Przenieœ do tablicy pod wskazany adres wartoœæ B

    inc r13         ; Inkrementuj kolumne
    jmp inner_loop      ; Skocz do pocz¹tku pêtli

mono:
    movzx rax, byte ptr [rsi + rbx + 0]       ; rbx = wartoœæ R
    movzx rcx, byte ptr [rsi + rbx + 1]   ; rcx = wartoœæ G
    movzx rdx, byte ptr [rsi + rbx + 2]   ; rdx = wartoœæ B

    movzx eax, al       ; Przenieœ pierwszy bajt do EAX i wyzeruj górne bity
    add eax, ecx        ; Dodaj drugi bajt to EAX
    add eax, edx        ; Dodaj trzeci bajt do EAX
    mov edx, 0          ; Wyzeruj górne bity (przygotowanie dla div)
    mov ecx, 3          ; Dzielnik = 3
    div ecx             ; EAX / ECX, wynik w EAX, reszta w EDX

    ; Oblicz pozycje w obrazie wyjsciowym
    mov rdi, r12                ; rdi = Youtput (indeks wiersza)
    imul rdi, r8                ; rdi = Youtput * outputWidth
    add rdi, r13                ; rdi += Xoutput (indeks kolumny)

    xor rdx, rdx                ; Wyzeruj RDX
    mov rdx, rdi                ; Skopiuj pozycjê bitow¹
    shr rdi, 3                  ; rdi = rdi / 8 (pozycja bajtu)
    and rdx, 7                  ; rdx = rdi % 8 (pozycja bitu)

    mov ecx, 127                ; Za³aduj wartoœæ 127 do ECX
    cmp eax, ecx                ; Porównaj wartoœæ w EAX z 127
    jg average_greater          ; Skocz je¿eli jest wiêcej ni¿ 127

    btr [r14 + rdi], rdx        ; Ustaw bit na 0
    jmp next_step               ; Skocz do zakoñczenia pêtli
        
average_greater:
    bts [r14 + rdi], rdx        ; Ustaw bit na 1
    jmp next_step               ; Skocz do zakoñczenia pêtli

_565:
    mov rdi, r12                ; rdi = Youtput (indeks wiersza)
    imul rdi, r8                ; rdi = Youtput * outputWidth
    add rdi, r13                ; rdi += Xoutput (indeks kolumny)
    imul rdi, rdi, 2            ; Przemnó¿ adres wyjœciowy razy dwa (Jeden px reprezentowany przez 2 bajty)

    movzx rax, byte ptr [rsi + rbx + 0]   ; rbx = wartoœæ R
    movzx rcx, byte ptr [rsi + rbx + 1]   ; rcx = wartoœæ G
    movzx rdx, byte ptr [rsi + rbx + 2]   ; rdx = wartoœæ B

    ; Przekszta³cenie R na 5 bitów (R[4:0])
    shr rax, 3                            ; rax = R >> 3 (5 bitów)

    ; Przekszta³cenie G na 6 bitów (G[5:0])
    shr rcx, 2                            ; rcx = G >> 2 (6 bitów)
    shl rcx, 5                            ; rcx = (G[5:0] << 5)

    ; Przekszta³cenie B na 5 bitów (B[4:0])
    shr rdx, 3                            ; rdx = B >> 3
    shl rax, 11                           ; rax = (R[4:0] << 11)

    ; Po³¹cz R, G, B w jeden 16-bitowy kolor RGB565
    or rax, rcx                           ; rax = R | G
    or rax, rdx                           ; rax = R | G | B (16-bitowy kolor)

    ; Zapisz wynik w pamiêci
    mov word ptr [r14 + rdi], ax           ; Zapisz RGB565 jako 2 bajty

next_step:
    inc r13                          ; PrzejdŸ do nastêpnej kolumny
    jmp inner_loop                   ; Powtórz dla kolejnego piksela

next_row:
    inc r12                     ; PrzejdŸ do nastêpnego wiersza
    jmp outer_loop              ; Powtórz dla kolejnego wiersza

done:
    ; Zakoñczenie
    pop rbx         ; Pobierz ze stosu wartoœæ RBX
    pop rdi         ; Pobierz ze stosu wartoœæ RDI
    pop rsi         ; Pobierz ze stosu wartoœæ RSI
    ret
ConvertASM endp
END