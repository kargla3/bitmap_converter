#include "pch.h"
#include "MyDLL.h"

int ConvertCPP(char* inputPtr, char* outputPtr, int inputWidth, int inputHeight, int outputWidth, int outputHeight, int mode)
{
    if (outputHeight == 0) {
        std::cerr << "Division by zero: outputHeight cannot be zero!" << std::endl;
        return 0;
    }

    double scaleX = static_cast<double>(inputWidth) / static_cast<double>(outputWidth);
    double scaleY = static_cast<double>(inputHeight) / static_cast<double>(outputHeight);

    for (int i = 0; i < outputHeight; i++) {
        for (int j = 0; j < outputWidth; j++) {
            int xInput = j * scaleX;
            int yInput = i * scaleY;

            uint8_t* inputPixel = reinterpret_cast<uint8_t*>(inputPtr + (yInput * inputWidth + xInput) * 3);

            uint8_t R = inputPixel[0];
            uint8_t G = inputPixel[1];
            uint8_t B = inputPixel[2];


            if (mode == 0) {
                uint8_t* outputPixel = reinterpret_cast<uint8_t*>(outputPtr) + (i * outputWidth + j) / 8;
                size_t bitIndex = (i * outputWidth + j) % 8;
                uint8_t color = static_cast<uint8_t>((R + G + B) / 3);
                if (color > 127)
                    *outputPixel |= (1 << (7 - bitIndex));
                else
                    *outputPixel &= ~(1 << (7 - bitIndex));
            }
            else if (mode == 565) {
                uint8_t* outputPixel = reinterpret_cast<uint8_t*>(outputPtr + (i * outputWidth + j) * 2);
                uint16_t rgb565 = ((R >> 3) << 11) | ((G >> 2) << 5) | (B >> 3);
                outputPixel[1] = static_cast<uint8_t>(rgb565 >> 8);
                outputPixel[0] = static_cast<uint8_t>(rgb565 & 0xFF);
            }
            else if (mode == 888) {
                uint8_t* outputPixel = reinterpret_cast<uint8_t*>(outputPtr + (i * outputWidth + j) * 3);
                outputPixel[2] = R;
                outputPixel[1] = G;
                outputPixel[0] = B;
            }
        }
    }

    return 1;
}