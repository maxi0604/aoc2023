# AOC 2023
## The plan
With descending priority:
- Solve the challenges
- Have solutions run in reasonable time
- Use a different language for each day
- If the above fails, use a different presentation for the language from before (e. g. local HTML site vs. console output)
- If applicable, get the code running in an environment similar to a real device/deployment environment (e. g. QEMU, after the code is done in ol' reliable [RARS](https://github.com/TheThirdOne/rars))
- Document the process used to get the code running if it's interesting.
- Come up with nice algorithms
- Come up with optimal (with respect to space/time complexity) algorithms

### Used already
- [x] x86-64 Assembly (d1, p1)
- [x] C (d1, p2)
- [x] Haskell (d2)
- [x] Rust (d3)
- [x] OCaml (d4)
- [x] C# (d5)
- [x] C++ (d6)
- [x] [JavaScript + HTML + CSS (d7)](https://maxi0604.github.io/aoc2023/d7)
- [x] Python (d8)
- [x] Julia (d9)
- [x] Kotlin (d10)
- [x] Java (d11)
### Ideas
- [ ] ARM Assembly
- [ ] Ada
- [ ] COBOL
- [ ] Clojure
- [ ] Common Lisp
- [ ] D
- [ ] Elixir
- [ ] Erlang
- [ ] F#
- [ ] Go
- [ ] Lua
- [ ] PHP
- [ ] Pascal
- [ ] R
- [ ] RISC-V Assembly
- [ ] Scala
- [ ] TypeScript
- [ ] Uiua
- [ ] VHDL
- [ ] Zig
- [ ] Zsh

Note that there are more languages than days here. This is deliberate.

## Resources
- https://wiki.freepascal.org/Basic_Pascal_Tutorial/Contents
- https://theintobooks.wordpress.com/2019/12/28/hello-world-on-risc-v-with-qemu/
- https://uclibc.org/docs/psABI-x86_64.pdf
- http://ocamlverse.net/content/monadic-parsers-angstrom.html
- https://novaspec.org/cl/
## License
MIT. Feel free to use the code presented here as e. g. a starting point for structuring your solutions or other projects. Please do not copy the solutions verbatim or use this repository to cheat.

## Log
- d1: Finished part 1 with **🖥️ x86-64 Assembly**, part 2 was becoming too depressing, so I wrote **🇨 C** instead. (Wow, wouldn't have expected to ever consider C as an upgrade...)
- d2: Finished part 1 and 2 with **λ Haskell**. After wasting way too much time on unsuccessfully trying to understand parser combinators, I just wrote a minor abomination using `splitOn`. The actual tasks were quite fun to implement (It's always fun to get to use `transpose`), however.
- d3: Wrote both parts in **🦀 Rust**. For part 1, I spent way too much time trying to debug some off-by-one error at the end of a line. Fixed it by just padding each line with dots internally. Part 2 went over easily. Using multiple binaries in the same crate made it really pleasant to split the code for both parts. The fact that rust considers a `String` to be valid UTF-8 could be worked around easily by just storing the input as bytes and converting to `&str` where needed. Hit the rate limit, after which it also doesn't ever tell you whether your attempts are too high or too low again (probably to prevent binary searching for the answer).
- d4: Wrote both parts in **🐫 OCaml**. This took longer than it would if I hadn't also stubbornly decided that today is the day on which I finally grok parser combinators (See above). Wrote a parser combinator and fought with a weird "Why is that of that type" bug and finished part one on the first submission. For part 2, I don't really see the functional solution, so I wrote a `for` loop that mutates an `int array`. I still don't know the difference between `in`, `;` and `;;` precisely but it works. Also finished part 2 on first submission. I definitely want to combine more parsers in the future, maybe I'll try `Parsec` again now. I didn't bother with making it build both binaries today, so you'll have to manually change `d4/dune` to do that.
- d5: Wrote both parts in **#️⃣ C#**. No complaints about the language, considering that it's a spiced up Java and my childhood programming language. Part 1 went over easily. Part 2 took forever because of my aversion to rewriting completely exactly when it would be the right time to do so, as well as imperative programming badness. All in all, got done though.
- d6: Wrote both parts in **➕ C++** while not at home. Bruteforce was way fast enough. Had integer overflow on part 2.
- d7: Had I known about the theme of this day beforehand, I would've reserved OCaml for it. Instead, I decided it would be fun to build a nice little page for it with **🌐 JavaScript, HTML and CSS**. While that probably took more time than actually writing the code, it was a fun experience. Also, I didn't think I'd be the one who falls for the "JavaScript `array.sort` meme" but here we are. I'd recommend to keep that in mind before you write JS. CSS and HTML were quite fun to write though and did show that some technical parts of the Internet are actually quite well-made. You can view the page [on GitHub pages.](https://maxi0604.github.io/aoc2023/d7) This also now means that this repository has a constant green CI checkmark since GitHub requires you set up a pipeline to publish static content directly without Jekyll. Maybe I'll set up CI for all the things at some point but maybe also not...
- d8: This one was bad. I didn't start early enough and finished at 2023-12-**09**T00:34, almost breaking the streak. My solution for part 2 also isn't correct in general but only works when each path is a perfect loop. Still, got two stars with **🐍 Python**
- d9: Sadly being really too late with this one, I completed both parts of this one at 2023-12-**10**T16:06 in **∴ Julia**. This was a fun one though and the implementation, while leaving out obvious optimizations, ended up being pretty clean.
- d10: Rekindling the streak, I solved day 10 on day 10, using **🇰 Kotlin** for both parts. The language is indeed a nice upgrade over Java, given that you can actually do mindblowing things such as indexing into an array or initializing a 2D array in something that a reasonable programmer would put on one line. Getting Kotlin to run outside of its preffered environment (i. e. IntelliJ IDEA) proved too difficult for today though, so I just set up a run config for each input. Also, way too much time was spent not reading the fact that junk pipes also count as potential nest area. The actual algorithm for finding the area was not too difficult though. I would've wished for some more advanced pattern matching in the language, like
```kotlin
when ((dir, cur)) {
    (Dir.Left, 'J') -> Dir.Up
}
```
but there's an open issue about this and it can be worked around somewhat painlessly. (See code)

Unrelated, but since GitHub limits the number of languages for which the percentage of the repository is calculated here's a screenshot from [onefetch](https://github.com/o2sh/onefetch) on day 10:

![Screenshot from onefetch, showing ● C# (17.4 %) ● Kotlin (15.3 %) ● Rust (13.8 %) ● JavaScript (11.2 %) ● Python (6.7 %) ● C (5.9 %) ● Assembly (5.4 %) ● OCaml (5.2 %) ● CSS (4.4 %) ● HTML (3.8 %) ● Haskell (3.8 %) ● C++ (3.4 %) ● Julia (3.4 %) ● Zsh (0.3 %)](res/d10langs.png)
- d11: Finished this after 00:00, however, I have decided to count a solution of being part of a streak if it was done before the next challenge is available. Today's solution is written in **☕ Java**, delivering programming language *somewhat-okayness* since 1995. After writing a solution to part 1 first try, I had to replace `int` with `long`, leading to `Point` becoming `Polong` initially. After fixing that, the solution worked smoothly though. I think few people would actually go for the inefficient solution for part 1 that requires you to rewrite in part 2, since modifying a data structure while iterating is a pain that we usually want to avoid. I also decided to relicense this under MIT today, since it occured to me that this might be a useful collection of what a somewhat minimal project in a lot of languages looks like and people might be deterred from using it like this because they'd have to license their project under the AGPL. I still think copyleft is valuable for larger projects, however, I found this diffstat quite notable:
```
 LICENSE        | 662 ++-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 ```
