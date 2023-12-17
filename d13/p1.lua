local function calc(lines)
    for i, line in ipairs(lines) do
        print(line)

        if line == lines[#lines - i + 1] then
        end
    end

end

while true do
    local lines = {};
    local line = io.read("l")

    if line == nil then
        break
    elseif line == "" then
        calc(lines)
    else
        lines[#lines+1] = line
    end
end

