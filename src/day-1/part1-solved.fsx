open System
open System.IO

type Image = int[]
type Example = { Label : int; Pixels: Image } 

let localPath = @"C:\Dev\GitHub\Workshop-Practical-Machine-Learning\src\day-1\"
let trainingPath = Path.Combine(localPath,"digits", "trainingsample.csv")
let validationPath = Path.Combine(localPath,"digits", "validationsample.csv")

let readExamples path = 
    File.ReadAllLines path
    |> Array.map (fun line -> line.Split(','))
    |> fun d -> d.[1 ..]
    |> Array.map(Array.map(int))
    |> Array.map(fun line ->{ Example.Label = line.[0]; Pixels= line.[1..] })


let training = readExamples trainingPath

let validation = readExamples validationPath

let distance (img1:Image) (img2:Image) =
    Array.map2 (fun p1 p2 -> (p1-p2)*(p1-p2)) img1 img2
    |> Array.sum
    |> float
    |> sqrt
 
let sillyClassifier(img : Image) = 7

let classifier(img : Image) =
    let closestExample=
        training
        |> Array.minBy(fun ex -> distance img ex.Pixels)
    closestExample.Label



let evaluate classifier =
    validation
    |> Array.averageBy(fun ex ->
        let predicted = classifier ex.Pixels
        let actual = ex.Label
        if predicted = actual
        then 1.0
        else 0.0)

evaluate classifier

