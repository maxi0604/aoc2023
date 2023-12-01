#include <stdio.h>
#include <stdlib.h>
#include <string.h>
int main(void)
{
    char *needles[] = {
        "zero",
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine",
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
    };

    long long res = 0;
    char line[512];
    while (fgets(line, 512, stdin)) {
        char* min_find = NULL;
        char* max_find = NULL;
        int mini = 0;
        int maxi = 10;
        for(int i = 0; i < 20; ++i) {
            char* needle = needles[i];

            char* loc = strstr(line, needle);
            if (loc && (!min_find || loc < min_find)) {
                min_find = loc;
                mini = i;
            }

            char* find = line;

            while (find = strstr(find, needle)) {
                if (find > max_find) {
                    max_find = find;
                    maxi = i;
                }
                find++;
            }
        }

        res += 10 * (mini % 10) + maxi % 10;
        printf("cur: %d\n", 10 * mini + maxi);
        printf("%lld\n", res);
    }

    return EXIT_SUCCESS;
}
