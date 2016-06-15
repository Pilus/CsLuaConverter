gsub = string.gsub;

function string.split(sep, str)
    local sep, fields = sep or ":", {}
    local pattern = string.format("([^%s]+)", sep)
        gsub(str, pattern, function(c) fields[#fields+1] = c end)
    return unpack(fields)
end
strsplit = string.split;
strrep = string.rep;

function string.join(del, ...)
    local t = {...};
    local s = "";
    for i = 1, #(t) do
        s = s .. t[i] or "nil";
        if i < #(t) then
            s = s..del;
        end
    end
    return s;
    end

function tContains(t, value)
    for _,v in pairs(t) do
        if (v == value) then
            return true;
        end
    end
    return false;
end

function print_r ( t )  
    local print_r_cache={}
    local function sub_print_r(t,indent)
        if (print_r_cache[tostring(t)]) then
            print(indent.."*"..tostring(t))
        else
            print_r_cache[tostring(t)]=true
            if (type(t)=="table") then
                for pos,val in pairs(t) do
                    if (type(val)=="table") then
                        print(indent.."["..pos.."] => "..tostring(t).." {")
                        sub_print_r(val,indent..string.rep(" ",string.len(pos)+8))
                        print(indent..string.rep(" ",string.len(pos)+6).."}")
                    elseif (type(val)=="string") then
                        print(indent.."["..pos..'] => "'..val..'"')
                    else
                        print(indent.."["..pos.."] => "..tostring(val))
                    end
                end
            else
                print(indent..tostring(t))
            end
        end
    end
    if (type(t)=="table") then
        print(tostring(t).." {")
        sub_print_r(t,"  ")
        print("}")
    else
        sub_print_r(t,"  ")
    end
    print()
end

dofile("CsLua.lua");

Lua.Strings.format = function(str,...)
    str =  gsub(str,"{(%d)}", function(n) return "%s" end);
    return string.format(str,...)
end

local bitLimit = 32;
function getBitTable(value)
    local t = {};
    for i = bitLimit,1,-1 do
        local c = math.pow(2, i);
        t[i] = value >= c/2;

        if t[i] then
            value = value - c/2;
        end
    end
    return t;
end

function getValueFromBitTable(t)
    local value = 0;
    for i = 1,bitLimit,1 do
        local c = math.pow(2, i);
        if t[i] then
            value = value + c/2;
        end
    end
    return value;
end

bit = {
    lshift = function(a,b)
        return a*math.pow(2, b);
    end,
    rshift = function(a,b)
        return math.floor(a/math.pow(2, b));
    end,
    band = function(a,b)
        local ta = getBitTable(a);
        local tb = getBitTable(b);
        local t = {};

        for i = 1,bitLimit,1 do
            t[i] = ta[i] and tb[i];
        end

        return getValueFromBitTable(t);
    end
};

dofile("CsLuaTest.lua");
