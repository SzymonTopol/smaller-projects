wejscie = input()

def spr(wejscie_1):
    wejscie = ''.join(wejscie_1)

    lewa_strona = wejscie.split('=')[0]
    prawa_strona = wejscie.split('=')[1]

    lewa_strona = lewa_strona.replace('-', '+-')
    prawa_strona = prawa_strona.replace('-', '+-')

    lewa_strona = lewa_strona.split('+')
    prawa_strona = prawa_strona.split('+')

    while '' in lewa_strona:
        lewa_strona.remove('')
    while '' in prawa_strona:
        prawa_strona.remove('')

    def oblicz_strone(strona):
        wynik = 0
        for i in strona:
            wynik += int(i)
        return wynik
    
    if oblicz_strone(lewa_strona) == oblicz_strone(prawa_strona):
        print(wejscie)
        exit()

        
wejscie = list(wejscie)

do_podniesienia = []
do_odlozenia = []
do_zmienienia_w_jednym = []

for i in range(len(wejscie)):
    match wejscie[i]:
        case '0':
            do_odlozenia.append(i)
            do_zmienienia_w_jednym.append(i)
        case '1':
            do_odlozenia.append(i)
        case '2':
            do_zmienienia_w_jednym.append(i)
        case '3':
            do_odlozenia.append(i)
            do_zmienienia_w_jednym.append(i)
        case '5':
            do_odlozenia.append(i)
            do_zmienienia_w_jednym.append(i)
        case '6':
            do_podniesienia.append(i)
            do_odlozenia.append(i)
            do_zmienienia_w_jednym.append(i)
        case '7':
            do_podniesienia.append(i)
        case '8':
            do_podniesienia.append(i)
        case '9':
            do_podniesienia.append(i)
            do_odlozenia.append(i)
            do_zmienienia_w_jednym.append(i)

for i in do_zmienienia_w_jednym:
    match wejscie[i]:
        case '0':
            wejscie[i] = '9'

            spr(wejscie)
                
            wejscie[i] = '6'
            
            spr(wejscie)
                    
            wejscie[i]='0'
        case '2':
            wejscie[i] = '3'
            spr(wejscie)

            wejscie[i]='2'
        case '3':
            wejscie[i] = '2'

            spr(wejscie)

            wejscie[i] = '5'
            
            spr(wejscie)

            wejscie[i]='3'
        case '5':
            wejscie[i] = '3'
            spr(wejscie)

            wejscie[i]='5'
        case '6':
            wejscie[i] = '0'
            spr(wejscie)

            wejscie[i] = '9'
            spr(wejscie)

            wejscie[i]='6'
        case '9':
            wejscie[i] = '0'
            spr(wejscie)

            wejscie[i] = '6'
            
            spr(wejscie)

            wejscie[i]='9'

def odlozenie(wejscie):
    for i in do_odlozenia:
        match wejscie[i]:
            case '0':
                wejscie[i] = '8'
                spr(wejscie)

                wejscie[i]='0'
            case '1':
                wejscie[i] = '7'
                spr(wejscie)

                wejscie[i]='1'
            case '3':
                wejscie[i] = '9'
                spr(wejscie)

                wejscie[i]='3'
            case '5':
                wejscie[i] = '6'
                spr(wejscie)

                wejscie[i]='9'
                
                spr(wejscie)

                wejscie[i]='5'
            case '6':
                wejscie[i] = '8'
                spr(wejscie)

                wejscie[i]='6'
            case '9':
                wejscie[i] = '8'
                spr(wejscie)

                wejscie[i]='9'

for i in do_podniesienia:
    match wejscie[i]:
        case '6':
            wejscie[i] = '5'

            odlozenie(wejscie)

            wejscie[i] = '6'

        case '7':
            wejscie[i] = '1'

            odlozenie(wejscie)

            wejscie[i] = '7'
        
        case '8':
            wejscie[i] = '0'

            odlozenie(wejscie)

            wejscie[i] = '6'

            odlozenie(wejscie)

            wejscie[i] = '9'

            odlozenie(wejscie)

            wejscie[i] = '8'

        case '9':
            wejscie[i] = '3'

            odlozenie(wejscie)

            wejscie[i] = '5'

            odlozenie(wejscie)

            wejscie[i] = '9'


print('no')