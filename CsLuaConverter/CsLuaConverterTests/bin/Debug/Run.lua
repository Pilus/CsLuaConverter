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

dofile("CsLuaTest.lua");
