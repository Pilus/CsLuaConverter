System.Collections.Generic.List = _M.NE({[1] = function(interactionElement, generics, staticValues)
    local implements = {
        System.Collections.IList.__typeof,
        System.Collections.Generic.IList[generics].__typeof,
        System.Collections.ICollection.__typeof,
        System.Collections.Generic.ICollection[generics].__typeof,
        System.Collections.IEnumerable.__typeof,
        System.Collections.Generic.IEnumerable[generics].__typeof,
        System.Collections.Generic.IReadOnlyList[generics].__typeof,
        System.Collections.Generic.IReadOnlyCollection[generics].__typeof,
    };
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('List','System.Collections.Generic',baseTypeObject,1,generics,implements,interactionElement,'Class', 1854);

    local getCount = function(element)
        return not(element[typeObject.level][0] == nil) and (#(element[typeObject.level]) + 1) or 0;
    end

    _M.IM(members,'ForEach',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        signatureHash = 2*4393*2*generics[1].signatureHash,
        types = {System.Action[{generics[1]}].__typeof},
        func = function(element,action)
            for i = 0,getCount(element)-1 do
                (action%_M.DOT)(element[typeObject.level][i]);
            end
        end,
    });

    _M.IM(members,'Count',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        numMethodGenerics = 0,
        signatureHash = 0,
        get = function(element)
            return getCount(element);
        end,
    });

    _M.IM(members,'Capacity',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        numMethodGenerics = 0,
        get = function(element)
            local c = getCount(element);
            return c == 0 and c or math.max(4, c);
        end,
    });

    local ThrowIfIndexNotNumber = function(element, index)
        if not(type(index) == "number") then
            _M.Throw(System.Exception("Attempted to index list with a non number index: "..tostring(index)));
        end
    end

    local ThrowIfOutOfRange = function(element, index)
        local c = getCount(element);
        if index < 0 or index >= c then
            _M.Throw(System.ArgumentOutOfRangeException._C_0_0());
        end
    end

    _M.IM(members,'#',{
        level = typeObject.Level,
        memberType = 'Indexer',
        scope = 'Public',
        --types = {generics[1]},
        get = function(element, index)
            ThrowIfIndexNotNumber(element, index);
            ThrowIfOutOfRange(element, index);
            return element[typeObject.level][index];
        end,
        set = function(element, index, value)
            ThrowIfIndexNotNumber(element, index);
            ThrowIfOutOfRange(element, index);
            element[typeObject.level][index] = value;
        end,
    });

    _M.IM(members,'Add',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        signatureHash = 2*generics[1].signatureHash;
        func = function(element,value)
            local c = getCount(element);
            element[typeObject.level][c] = value;
            return c;
        end,
    });

    _M.IM(members,'Add',{  --  IList.Add(system.object)
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        signatureHash = 8572;
        func = function(element,value)
            local c = getCount(element);
            element[typeObject.level][c] = value;
            return c;
        end,
    });
    
    _M.IM(members,'AddRange',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        signatureHash = 2*System.Collections.Generic.IEnumerable[generics].__typeof.signatureHash,
        numMethodGenerics = 0,
        func = function(element,value)
            local c = getCount(element);
            for _,v in (value  % _M.DOT).GetEnumerator() do
                element[typeObject.level][c] = v;
                c = c + 1;
            end
        end,
    });

    _M.IM(members,'GetEnumerator',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {},
        numMethodGenerics = 0,
        signatureHash = 0,
        func = function(element)
            return function(_, prevKey) 
                local key;
                if prevKey == nil then
                    key = 0;
                else
                    key = prevKey + 1;
                end

                if key < getCount(element) then
                    return key, element[typeObject.level][key];
                end
                return nil, nil;
            end;
        end,
    });
    

    _M.IM(members,'IsFixedSize',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        get = function(element)
            return false;
        end,
    });

    _M.IM(members,'IsReadOnly',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        get = function(element)
            return false;
        end,
    });

    _M.IM(members,'IsSynchronized',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        get = function(element)
            return false;
        end,
    });

    _M.IM(members,'SyncRoot',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        get = function(element)
            return System.Object._C_0_0();
        end,
    });

    _M.IM(members,'Clear',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        signatureHash = 0;
        --types = {},
        func = function(element)
            element[typeObject.level] = {};
        end,
    });

    _M.IM(members,'Contains',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        --types = {generics[1]},
        signatureHash = 2*generics[1].signatureHash;
        func = function(element,value)
            for i = 0,getCount(element)-1 do
                if (element[typeObject.level][i] % _M.DOT).Equals_M_0_8572(value) then
                    return true;
                end
            end
            return false;
        end,
    });

    _M.IM(members,'Find',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        --types = {System.Predicate[generics].__typeof},
        signatureHash = 2*(System.Predicate[generics].__typeof).signatureHash,
        func = function(element,f)
            for i = 0,getCount(element)-1 do
                local v = element[typeObject.level][i];
                if (f % _M.DOT)(v) then
                    return v;
                end
            end
        end,
    });

    _M.IM(members,'FindIndex',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        --types = {System.Predicate[generics].__typeof},
        signatureHash = 2*(System.Predicate[generics].__typeof).signatureHash,
        func = function(element,f)
            for i = 0,getCount(element)-1 do
                local v = element[typeObject.level][i];
                if (f % _M.DOT)(v) then
                    return i;
                end
            end
        end,
    });

    _M.IM(members,'FindLast',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        --types = {System.Predicate[generics].__typeof},
        signatureHash = 2*(System.Predicate[generics].__typeof).signatureHash,
        func = function(element,f)
            for i = getCount(element)-1,0,-1 do
                local v = element[typeObject.level][i];
                if (f % _M.DOT)(v) then
                    return v;
                end
            end
        end,
    });

    _M.IM(members,'FindLastIndex',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        --types = {System.Predicate[generics].__typeof},
        signatureHash = 2*(System.Predicate[generics].__typeof).signatureHash,
        func = function(element,f)
            for i = getCount(element)-1,0,-1 do
                local v = element[typeObject.level][i];
                if (f % _M.DOT)(v) then
                    return i;
                end
            end
        end,
    });

    _M.IM(members,'FindAll',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        --types = {System.Predicate[generics].__typeof},
        signatureHash = 2*(System.Predicate[generics].__typeof).signatureHash,
        func = function(element,f)
            local list = System.Collections.Generic.List[generics]._C_0_0();
            for i = 0,getCount(element)-1 do
                local v = element[typeObject.level][i];
                if (f % _M.DOT)(v) then
                    (list % _M.DOT).Add_M_0_8572(v);
                end
            end
            return list;
        end,
    });

    _M.IM(members,'IndexOf',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        --types = {generics[1]},
        signatureHash = 2*generics[1].signatureHash,
        func = function(element,value)
            for i = 0,getCount(element)-1 do
                local v = element[typeObject.level][i];
                if (v % _M.DOT).Equals_M_0_8572(value) then
                    return i;
                end
            end
            return -1;
        end,
    });

    _M.IM(members,'LastIndexOf',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        --types = {generics[1]},
        signatureHash = 2*generics[1].signatureHash,
        func = function(element,value)
            for i = getCount(element)-1,0,-1 do
                local v = element[typeObject.level][i];
                if (v % _M.DOT).Equals_M_0_8572(value) then
                    return i;
                end
            end
        end,
    });

    _M.IM(members,'Insert',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        types = {System.Int.__typeof, generics[1]},
        signatureHash = 2*System.Int.__typeof.signatureHash + 3*generics[1].signatureHash,
        func = function(element,index,value)
            for i = getCount(element)-1,index,-1 do
                element[typeObject.level][i+1] = element[typeObject.level][i];
            end
            element[typeObject.level][index] = value;
        end,
    });

    _M.IM(members,'GetRange',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        --types = {System.Int.__typeof, System.Int.__typeof},
        signatureHash = 2*System.Int.__typeof.signatureHash + 3*System.Int.__typeof.signatureHash,
        func = function(element, start, num)
            local list = System.Collections.Generic.List[generics]._C_0_0();
            for i = start, start + num - 1 do
                (list % _M.DOT).Add_M_0_8572(element[typeObject.level][i]);
            end
            return list;
        end,
    });

    _M.IM(members,'InsertRange',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        --types = {System.Int.__typeof, System.Collections.Generic.IEnumerable[generics].__typeof},
        signatureHash = 2*System.Int.__typeof.signatureHash + 3*System.Collections.Generic.IEnumerable[generics].__typeof.signatureHash,
        func = function(element,start, value)
            local count = 0;
            for _,v in (value  % _M.DOT).GetEnumerator() do
                count = count + 1;
            end
            
            for i = getCount(element)-1,start,-1 do
                element[typeObject.level][i+count] = element[typeObject.level][i];
            end

            local c = start;
            for _,v in (value  % _M.DOT).GetEnumerator() do
                element[typeObject.level][c] = v;
                c = c + 1;
            end
        end,
    });

    _M.IM(members,'Remove',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        --types = {generics[1]},
        signatureHash = 2*generics[1].signatureHash,
        func = function(element, obj)
            local index = (element % _M.DOT).IndexOf(obj);
            if index < 0 then
                return false;
            end
            local count = getCount(element);
            for i = index, count do
                element[typeObject.level][i] = element[typeObject.level][i+1];
            end
            return true;
        end,
    });

    _M.IM(members,'RemoveRange',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        --types = {System.Int.__typeof, System.Int.__typeof},
        signatureHash = 2*System.Int.__typeof.signatureHash + 3*System.Int.__typeof.signatureHash,
        func = function(element, start, num)
            local count = getCount(element);
            for i = start, count do
                element[typeObject.level][i] = element[typeObject.level][i+num];
            end
        end,
    });

    _M.IM(members,'RemoveAt',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        signatureHash = 3926,
        func = function(element, index)
            ThrowIfIndexNotNumber(element, index);
            ThrowIfOutOfRange(element, index);
            local count = getCount(element);
            for i = index, count do
                element[typeObject.level][i] = element[typeObject.level][i+1];
            end
        end,
    });


    _M.IM(members, '', {
        level = typeObject.Level,
        memberType = 'Cstor',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 0,
        scope = 'Public',
        func = function(element)
        end,
    });

    _M.IM(members, '', {
        level = typeObject.Level,
        memberType = 'Cstor',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 2*System.Collections.Generic.IEnumerable[{generics[1]}].__typeof.signatureHash,
        scope = 'Public',
        func = function(element, values)
            local c = 0;
            for _,v in (values %_M.DOT).GetEnumerator() do
                element[typeObject.level][c] = v;
                c = c + 1;
            end
        end,
    });

    _M.IM(members, '', {
        level = typeObject.Level,
        memberType = 'Cstor',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 2*Lua.Function.__typeof.signatureHash,
        scope = 'Public',
        func = function(element, values)
            local c = 0;
            for _,v in values do
                element[typeObject.level][c] = v;
                c = c + 1;
            end
        end,
    });

    local initialize = function(element, values)
        for i=1,#(values) do
            element[typeObject.level][i-1] = values[i];
        end
    end

    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end

    return "Class", typeObject, members, constructors, objectGenerator, implements, initialize;
end})
