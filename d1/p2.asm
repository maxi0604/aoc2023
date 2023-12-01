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
	  extern strstr
	  extern strrev

          section   .text
main:
outer:
	  mov       qword [index], in_str
inner:
          mov       rax, 0                  ; system call for read
          mov       rdi, 0                  ; file handle 0 is stdin
          mov       rsi, char               ; address of string to input to
          mov       rdx, 1                  ; number of bytes
          syscall                           ; read one byte
          cmp       rax, 0                  ; check if no bytes were read (EOF)
          jz        quit
          cmp       byte [char], 10         ; check for \n
          jz        break_read
	  mov       ax, [char]
	  mov       rdx, [index]
	  mov       [rdx], ax
	  inc       qword [index]
	  jmp       inner
break_read:
	  mov       rdx, [index]
	  mov       byte [rdx], 0
	  ; todo process

	  mov rdi, in_str
	  mov rsi, one
	  call strstr
	  cmp rax, 0
	  add [result], 1
	  no_one:


	  jmp outer
quit:
	  mov       rdi, printf_str
	  mov       rsi, [result]
	  xor       rax, rax
	  call      printf

          mov       rax, 60                 ; system call for exit
          xor       rdi, rdi                ; exit code 0
          syscall                           ; invoke operating system to exit

          section   .data
char:   db        0      ; current char
result: dq        0
index:   dq       0
digit:   dq       0
in_str: db "                                                                               " ; enough spaces
printf_str: db "%d", 10, 0
printf_ptr: db "%p", 10, 0

zero: db "zero", 0
one: db "one", 0
two: db "two", 0
three: db "three", 0
four: db "four", 0
five: db "five", 0
six: db "six", 0
seven: db "seven", 0
eight: db "eight", 0
nine: db "nine", 0
needles: dq one, two, three, four, five, six, seven, eight, nine
