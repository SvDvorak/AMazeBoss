using System;

public class FieldReferenceException : Exception
{
    public FieldReferenceException(string gameObjectName, string fieldName) : base(gameObjectName + " is missing field reference for " + fieldName)
    {
    }
}