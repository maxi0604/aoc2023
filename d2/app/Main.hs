module Main where

import Data.List
import Data.List.Split

main :: IO ()
main = do
    content <- getContents
    let linesOfFile = lines content
    let lexed = map lexLine linesOfFile
    print lexed
    let parsed = map parseLine lexed
    print parsed
    --
    print $ sumLegal parsed
    print $ sum $ map (product . minForGame) parsed

-- All elements fulfil condition and at least one exists.
all' :: (a -> Bool) -> [a] -> Bool
all' f x = all f x && not (null x)

parseLine :: RawGame -> Game
parseLine = map parseTriple

lexLine :: String -> RawGame
lexLine = map (map (splitOn " ") . splitOn ", ") . splitOn "; " . last . splitOn ": "

parseTriple :: [[String]] -> [Int]
parseTriple = map sum . transpose . map parsePair

parsePair :: [String] -> [Int]
parsePair [r, "red"] = [read r, 0, 0]
parsePair [g, "green"] = [0, read g, 0]
parsePair [b, "blue"] = [0, 0, read b]

type Game = [[Int]]
type RawGame = [[[String]]]

gameIsLegal :: Game -> Bool
gameIsLegal a = all (uncurry (<=)) (zip (minForGame a) [12, 13, 14])

minForGame :: Game -> [Int]
minForGame = map maximum . transpose

sumLegal :: [Game] -> Int
sumLegal = sum . map fst . filter (gameIsLegal . snd) . enum1

enum1 :: [b] -> [(Int, b)]
enum1 = zip [1..]
