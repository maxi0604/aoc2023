; https://cs.lmu.edu/~ray/notes/nasmtutorial/
; I will never complain about cc again, this is absolutely horrendous compared to writing C.
; ----------------------------------------------------------------------------------------
; Writes "Hello, World" to the console using only system calls. Runs on 64-bit Linux only.
; To assemble and run:
;
;     nasm -felf64 hello.asm && ld hello.o && ./a.out
; ----------------------------------------------------------------------------------------

; global    _start
global main
extern printf

section   .text
main:
outer:
    mov       rax, [digit]
    add       [result], rax
was_complete:
    mov       byte [flag], 0
inner:
    mov       rax, 0                  ; system call for read
    mov       rdi, 0                  ; file handle 0 is stdin
    mov       rsi, char               ; address of string to input to
    mov       rdx, 1                  ; number of bytes
    syscall                           ; read one byte
    cmp       rax, 0                  ; check if no bytes were read (EOF)
    jz        quit
    cmp       byte [char], 10              ; check for \n
    jz outer
    cmp byte  [char], 58              ; check for nondigit
    jg inner                          ; restart loop if not number
    movzx     rax, byte [char]
    sub       rax, 48                  ; 48 is ascii for '0'
    mov       qword [digit], rax
    cmp       byte [flag], 0
    jnz       delay               ; 10s digit was read already. store last dig in [digit] only.
    mov       rbx, 10
    mul       rbx                     ; shift left one digit otherwise
    ; mov       rdi, printf_str        ; printf
    ; mov       rsi, rax
    ; xor       rax, rax
    ; call      printf
    add       [result], rax
    ; mov       rdi, printf_str        ; printf
    ; mov       rsi, [result]
    ; xor       rax, rax
    ; call      printf
    delay:
    inc       byte [flag]                   ; bump flag to read ones digit next.
    jmp       inner

quit:
    mov       rax, [result]
    mov       rbx, 10
    mov       rcx, out_str
    add       rcx, 14

    mov       rdi, printf_str
    mov       rsi, [result]
    xor       rax, rax
    call      printf
    ; output_loop:                 ; write base 10 number to out
    ;     div       rbx
    ;     add       dx, 48
    ;     mov       [rcx], dx
    ;     sub       rcx, 1
    ;     cmp       rax, 0
    ;     jnz       output_loop
    ;
    ;           mov       rax, 1                  ; system call for write
    ;           mov       rdi, 1                  ; file handle 1 is stdout
    ;           mov       rsi, out_str            ; address of string to output
    ;           mov       rdx, 15                 ; number of bytes
    ;           syscall                           ; write out_str

    mov       rax, 60                 ; system call for exit
    xor       rdi, rdi                ; exit code 0
    syscall                           ; invoke operating system to exit

section   .data
    char:   db        0      ; current char
    result: dq        0
    flag:   db        2
    digit:   dq       0
    out_str: db "               "    ; 15 spaces
    printf_str: db "%d", 10
