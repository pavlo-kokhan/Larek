-> main

=== main ===
Hello there, traveler! What brings you here?
    * [I'm looking for supplies.]
        -> supplies
    * [Just passing by.]
        -> passing_by

=== supplies ===
Ah, supplies! We have the finest wares in town.
    * [Show me your inventory.]
        -> inventory
    * [Nevermind, I changed my mind.]
        -> end_dialogue

=== passing_by ===
Safe travels, friend. Be wary of the dangers ahead.
-> end_dialogue

=== inventory ===
Here’s what we have: 
- Sword
- Shield
- Potion
    * [I’ll take the sword.]
        -> chosen("Sword")
    * [I’ll take the shield.]
        -> chosen("Shield")
    * [I’ll take the potion.]
        -> chosen("Potion")

=== chosen(option) ===
You selected: {option}.
Thank you for your purchase!
-> END

=== end_dialogue ===
If you need anything, feel free to return.
-> END


