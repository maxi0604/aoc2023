#!/usr/bin/zsh
nasm -felf64 p1.asm
gcc --no-pie -o p1 p1.o
./p1
