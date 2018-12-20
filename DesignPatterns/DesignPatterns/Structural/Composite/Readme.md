### Composite Design Pattern

## Intent
The Composite composes objects into tree structures and lets clients treat individual objects and compositions uniformly. 

- Compose objects into tree structures to represent whole-part hierarchies. Composite lets clients treat individual objects and compositions of objects uniformly.
- Recursive composition
- “Directories contain entries, each of which could be a directory.”
- 1-to-many “has a” up the “is a” hierarchy

## Problem
Application needs to manipulate a hierarchical collection of “primitive” and “composite” objects. Processing of a primitive object is handled one way, and processing of a composite object is handled differently. Having to query the “type” of each object before attempting to process it is not desirable.

## Check list
1. Ensure that your problem is about representing “whole-part” hierarchical relationships.
2. Consider the heuristic, "Containers that contain containees, each of which could be a container." For example, "Assemblies that contain components, each of which could be an assembly." Divide your domain concepts into container classes, and containee classes.
3. Create a “lowest common denominator” interface that makes your containers and containees interchangeable. It should specify the behavior that needs to be exercised uniformly across all containee and container objects.
4. All container and containee classes declare an “is a” relationship to the interface.
5. All container classes declare a one-to-many “has a” relationship to the interface.
6. Container classes leverage polymorphism to delegate to their containee objects.
7. Child management methods [e.g. addChild(), removeChild()] should normally be defined in the Composite class. Unfortunately, the desire to treat Leaf and Composite objects uniformly may require that these methods be promoted to the abstract Component class. See the Gang of Four for a discussion of these “safety” versus “transparency” trade-offs.

## Rules of thumb
- Composite and Decorator have similar structure diagrams, reflecting the fact that both rely on recursive composition to organize an open-ended number of objects.
- Composite can be traversed with Iterator. Visitor can apply an operation over a Composite. Composite could use Chain of Responsibility to let components access global properties through their parent. It could also use Decorator to override these properties on parts of the composition. It could use Observer to tie one object structure to another and State to let a component change its behavior as its state changes.
- Composite can let you compose a Mediator out of smaller pieces through recursive composition.
- Decorator is designed to let you add responsibilities to objects without subclassing. Composite’s focus is not on embellishment but on representation. These intents are distinct but complementary. Consequently, Composite and Decorator are often used in concert.
- Flyweight is often combined with Composite to implement shared leaf nodes.