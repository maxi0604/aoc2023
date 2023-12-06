#include <cmath>
#include <sstream>
#include <string>
#include <iostream>
int main (int argc, char *argv[]) {
    std::string times;
    std::string dists;
    std::getline(std::cin, times);
    std::getline(std::cin, dists);
    std::cin >> dists;

    auto tstream = std::stringstream(times);
    auto dstream = std::stringstream(dists);

    std::string waste;
    int t, d;
    
    tstream >> waste;
    dstream >> waste;
    long prod = 1;
    while (tstream >> t) {
        dstream >> d;
        std::cout << "t: " << t << " d: " << d << std::endl;
        long ct = 0;
        for (long i = 0; i <= t; i++) {
            long x = i * (t - i);

            if (x > d)
                ct++;
        }
        std::cout << "ct:" << ct << std::endl;
        prod *= ct;
    }

    std::cout << "prod: " << prod << std::endl;

    return 0;
}   
