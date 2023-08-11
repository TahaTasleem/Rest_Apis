namespace RestApis.Repository
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]

    sealed class StoredProcedureParameterAttribute:Attribute
    {
    }
}
