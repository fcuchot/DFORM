using FluentAssertions;
using IntechCode.IntechCollection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntechCode.Tests.Animals
{
    public class AnimalTests
    {
         [Fact]
        public void The_array_is_not_a_safe_beast()
        {
            Animal[] animals = new Animal[5];
            animals[0] = new Dog();
            animals[1] = new Cat();

            object[] aO = animals;


            Action bug = () => aO[2] = "Hello!";
            bug.ShouldThrow<ArrayTypeMismatchException>();
        }

        public void co_and_contra_variance()
        {
            IMyEnumerable<Dog> eDog = null;
            IMyEnumerable<Animal> eAnimal = null;
            DumpAnimals(eAnimal);
            DumpAnimals(eDog);
        }

        static void DumpAnimals(IMyEnumerable<Animal> animals)
        {
        }
        static void DumpDogs(IMyEnumerable<Dog> dogs)
        {
        }

    }
}
