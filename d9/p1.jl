lines = readlines()

function calc_line(line::AbstractString)
    values = map((x) -> parse(Int, x), split(line, " "; keepempty=false))
    for i ∈ range(0, length(values) - 1)
        nonzero = false
        for j ∈ range(1, length(values) - i - 1)
            println(i, j)
            values[j] = values[j + 1] - values[j]
            nonzero = nonzero || (values[j] ≠ 0)
        end
        println(values)
    end
    return sum(values)
end

res = sum(calc_line, lines)
println("res:", res)