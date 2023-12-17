import scala.collection.mutable.ArrayBuffer
import scala.io.Source
case class Lens(var name: String, var value: Int) {
  def hash(): Int =
    name.map(c => c.toInt).foldLeft(0) {
      (e, acc) => ((acc + e) * 17) % 256
    }
}





object Part1 {
  def main(args: Array[String]) = {
    println("Hello, world")


    val input = Source.fromInputStream(System.in).mkString;
    val split = input.replace("\n", "").split(',')
    println(split.mkString("Array(", ", ", ")"))

    println("p1")
    val res = split.map(x => x.map(c => c.toInt).foldLeft(0) {
      (acc, e) => ((acc + e) * 17) % 256
    })

    println(res.mkString("Array(", ", ", ")"))
    val sum = res.sum
    printf("Sum: %s%n", sum)
    println("p2")
    val lenses = Array.fill(256) {ArrayBuffer.empty[Lens]}
    // lenses(2) += Lens("foo", 1)
  }
}