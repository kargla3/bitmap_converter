#pragma once
#ifndef MYDLL_H
#define MYDLL_H
#include <iostream>

extern "C" __declspec(dllexport)
int ConvertCPP(char* inputPtr, char* outputPtr,
    int inputWidth, int inputHeight,
    int outputWidth, int outputHeight, int mode);

#endif // !MYDLL_H
