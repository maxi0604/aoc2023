module Main where

import Data.List
import Data.Char (isDigit)
import Data.List.Split

main :: IO ()
main = do
    content <- readFile "/dev/stdin"
    let linesOfFile = lines content
    let lexed = map lexLine linesOfFile
    print lexed
    let parsed = map parseLine lexed
    print parsed
    --
    print $ sumLegal parsed
    return ()

-- All elements fulfil condition and at least one exists.
all' :: (a -> Bool) -> [a] -> Bool
all' f x = all f x && not (null x)

parseLine :: RawGame -> Game
parseLine = map parseTriple

lexLine :: String -> RawGame
lexLine = map (map (splitOn " ")) . map (splitOn ", ") . splitOn "; " . last . splitOn ": "

parseTriple :: [[String]] -> [Int]
parseTriple = map sum . transpose . map parsePair

parsePair :: [String] -> [Int]
parsePair [r, "red"] = [read r, 0, 0]
parsePair [g, "green"] = [0, read g, 0]
parsePair [b, "blue"] = [0, 0, read b]

type Game = [[Int]]
type RawGame = [[[String]]]

maxLegal :: Game -> [Int]
maxLegal = map maximum . transpose

gameIsLegal :: Game -> Bool
gameIsLegal a = all (uncurry (<=)) (zip (maxLegal a) [12, 13, 14])

sumLegal :: [Game] -> Int
sumLegal = sum . map fst . filter (gameIsLegal . snd) . enum1

enum1 :: [b] -> [(Int, b)]
enum1 = zip [1..]
