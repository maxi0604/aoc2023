module Main where

import Data.List
import Data.Char (isDigit)
import Data.List.Split

main :: IO ()
main = do
    content <- readFile "/dev/stdin"
    let linesOfFile = lines content
    let parsed = map parseLine linesOfFile
    print $ sumLegal parsed
    return ()


parseLine :: String -> Game
parseLine = chunksOf 3 . map read . tail . splitWhen (not . isDigit)

type Game = [[Int]]

maxLegal :: Game -> [Int]
maxLegal = map maximum . transpose

gameIsLegal :: Game -> Bool
gameIsLegal a = all (uncurry (<=)) (zip (maxLegal a) [12, 13, 14])

sumLegal :: [Game] -> Int
sumLegal = sum . map fst . filter (gameIsLegal . snd) . enum1

enum1 :: [b] -> [(Int, b)]
enum1 = zip [1..]
