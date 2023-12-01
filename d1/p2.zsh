#!/usr/bin/zsh
nasm -felf64 p2.asm
gcc -o p2 p2.o
./p2
