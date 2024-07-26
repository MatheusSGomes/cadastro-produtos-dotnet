using Flunt.Validations;

namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; private set; }
    public bool Active { get; set; }

    public Category(string name, string createdBy, string editedBy)
    {
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "Name", "Nome é obrigatório")
            .IsGreaterThan(Name, 3, "Name", "Número de caracteres do nome deve ser superior a 3")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy", "Nome de quem criou o a categoria é obrigatório")
            .IsNotNullOrEmpty(EditedBy, "EditedBy", "Nome de quem editou a categoria é obrigatório");

        // .IsNullOrEmpty(email, "Email").IsEmail(email, "Email");

        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active)
    {
        Name = name;
        Active = active;

        Validate();
    }
}