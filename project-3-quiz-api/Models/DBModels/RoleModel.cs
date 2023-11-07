using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace project_3_quiz_api.Models.DBModels
{
    public class RoleModel
    {
        // Primary Key
        [Key]
        public Guid Id { get; set; }
        // Properties
        [NotNull]
        public string Name { get; set; }

        // Navigation Properties
        public IEnumerable<UserModel> Users { get; set; }
    }
}
