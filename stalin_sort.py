import random

def main():
    not_int = True
    while not_int:
        try:
            amount = input("Jaką długość ma mieć tabela? (podaj liczbę naturalną): ")
            rang = input("Jaką maksymalną wartość mają przyjmować dane w tabeli? (podaj liczbę naturalną): ")
            amount = int(amount)
            rang = int(rang)
            not_int = False
        except ValueError:
            print("Podaj liczbę naturalną!")
            
    n_tab = new_list(amount,rang)

    print("Nieposortowana: ", end="")
    print(n_tab)

    print("Posortowana: ", end="")
    print(stalin_sort(n_tab))

def new_list(amount, rang):
    new_tab =[]
    for i in range(amount):
        new_tab.append(random.randint(0,rang))
    return new_tab

def stalin_sort(n_tab=[]):
    sorted = []
    z = 0
    for i in range(len(n_tab)):
        if i ==  0:
            sorted.append(n_tab[i])
        elif n_tab[z] <= n_tab[i]:
            z = i
            sorted.append(n_tab[i])

    return(sorted)

main()