~ temp language = "en"
-> main

=== main ===
{
- language == "en":
    -> main_en
- language == "ua":
    -> main_ua
- language == "ru":
    -> main_ru
}

=== main_en ===
Hello there, traveler! What brings you here?
* I'm looking for supplies.
    -> supplies_en
* Just passing by.
    -> passing_by_en

=== supplies_en ===
Ah, supplies! We have the finest wares in town.
* Show me your inventory.
    -> inventory_en
* Nevermind, I changed my mind.
    -> end_dialogue_en

=== passing_by_en ===
Safe travels, friend. Be wary of the dangers ahead.
-> end_dialogue_en

=== inventory_en ===
Here’s what we have: 
- Sword
- Shield
- Potion
* I’ll take the sword.
    -> chosen_en("Sword")
* I’ll take the shield.
    -> chosen_en("Shield")
* I’ll take the potion.
    -> chosen_en("Potion")

=== chosen_en(option) ===
You selected: {option}.
Thank you for your purchase!
-> END

=== end_dialogue_en ===
If you need anything, feel free to return.
-> END

=== main_ua ===
Привіт, мандрівнику! Що привело тебе сюди?
* Я шукаю припаси.
    -> supplies_ua
* Просто проходжу повз.
    -> passing_by_ua

=== supplies_ua ===
Ах, припаси! У нас найкращі товари в місті.
* Покажіть свій інвентар.
    -> inventory_ua
* Неважливо, я передумав.
    -> end_dialogue_ua

=== passing_by_ua ===
Щасливої дороги, друже. Остерігайтеся небезпек попереду.
-> end_dialogue_ua

=== inventory_ua ===
Ось що у нас є: 
- Меч
- Щит
- Зілля
* Я візьму меч.
    -> chosen_ua("Меч")
* Я візьму щит.
    -> chosen_ua("Щит")
* Я візьму зілля.
    -> chosen_ua("Зілля")

=== chosen_ua(option) ===
Ви обрали: {option}.
Дякуємо за покупку!
-> END

=== end_dialogue_ua ===
Якщо вам щось знадобиться, звертайтеся.
-> END

=== main_ru ===
Здравствуйте, путник! Что вас сюда привело?
* Я ищу припасы.
    -> supplies_ru
* Просто прохожу мимо.
    -> passing_by_ru

=== supplies_ru ===
Ах, припасы! У нас лучшие товары в городе.
* Покажите свой инвентарь.
    -> inventory_ru
* Неважно, я передумал.
    -> end_dialogue_ru

=== passing_by_ru ===
Счастливого пути, друг. Остерегайтесь опасностей впереди.
-> end_dialogue_ru

=== inventory_ru ===
Вот что у нас есть: 
- Меч
- Щит
- Зелье
* Я возьму меч.
    -> chosen_ru("Меч")
* Я возьму щит.
    -> chosen_ru("Щит")
* Я возьму зелье.
    -> chosen_ru("Зелье")

=== chosen_ru(option) ===
Вы выбрали: {option}.
Спасибо за покупку!
-> END

=== end_dialogue_ru ===
Если вам что-то понадобится, возвращайтесь.
-> END
