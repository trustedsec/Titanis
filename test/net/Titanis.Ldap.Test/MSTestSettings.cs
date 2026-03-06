#if DEBUG

#else
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]
#endif
