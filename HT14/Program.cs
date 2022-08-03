var cat = new Cat("Meow");
Animal kitten = new Kitten("eeeee");
cat.Speak();
kitten.Speak();


public class Animal
{
    string Sound { get; set; }

    public Animal(string sound)
    {
        Sound = sound;
    }

    public virtual void Speak()
    {
        Console.WriteLine(Sound);
    }
}


public class Cat : Animal
{
    public Cat(string sound) : base(sound)
    {

    }

    new public void Speak()
    {
        Console.WriteLine("MEOW");
    }
}

public class Kitten : Cat
{
    public Kitten(string sound) : base(sound)
    {

    }
}


