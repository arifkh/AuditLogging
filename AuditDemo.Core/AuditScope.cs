namespace AuditDemo.Core
{
    public enum AuditScope
    {
        /// <summary>
        /// Only track changes at the entity level. Such changes would be limited to adding or deleting the entity.
        /// </summary>
        ClassOnly,

        /// <summary>
        /// Only track changes to property values. Such changes would be limited to modifying the entity.
        /// </summary>
        PropertiesOnly,

        /// <summary>
        /// Track changes at both the entity and property levels. Including adding, deleting and modifying entities.
        /// </summary>
        ClassAndProperties
    }
}
