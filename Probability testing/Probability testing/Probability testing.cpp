#include <iostream>
#include <stdexcept>
#include <cassert>
#include <random>

using namespace std;

mt19937 rng{ random_device{}()};

constexpr int tests = 200000;

int single_test(int SIZE, int to_rand) {
    
    uniform_int_distribution<int> dist(1, SIZE-1);

    int seed = 0, cost = 0;

    for (int i = 0; i < SIZE; i++)
    {

        if (i >= to_rand)
        {
            cost += 30;
        }
        else {
            do
            {
                cost += 10;
                seed = dist(rng);

            } while (seed < i);
        }
    }

    return cost;
}

void calc(int SIZE, int to_rand, int *wsk_avg, int *wsk_high, int *wsk_low) {

    int wyniki[tests] = {};

    *wsk_high = wyniki[0];
    *wsk_low = tests;

    int avg = 0;

    for (int i = 0; i < tests; i++)
    {
        wyniki[i] = single_test(SIZE, to_rand);

        if (wyniki[i] >= *wsk_high)
        {
            *wsk_high = wyniki[i];
        }
        else if (wyniki[i] < *wsk_low) {
            *wsk_low = wyniki[i];
        }
        avg += wyniki[i];
    }

    *wsk_avg = avg/tests;
}

int main()
{
    constexpr int SIZE = 144; //ilość figurek
    
    int average_cost[SIZE+1] = {};
    int biggest_cost[SIZE+1] = {};
    int lowest_cost[SIZE+1] = {}; 

    int* wsk[3] = { average_cost,biggest_cost,lowest_cost };
    
    for (int i = 0; i <= SIZE; i++)
    {
        calc(SIZE, i, wsk[0], wsk[1], wsk[2]);
        wsk[0]++;
        wsk[1]++;
        wsk[2]++;
        cout << "Test " << i << "\n";
    }
    
    //Sortowanie danych

    int position[3] = { 0,0,0 };

    int avg_result = average_cost[0];
    int big_result = biggest_cost[0];
    int low_result = lowest_cost[0];

    for (int i = 0; i <= SIZE; i++)
    {
        if (average_cost[i] < avg_result)
        {
            avg_result = average_cost[i];
            position[0] = i;
        }
        
        if (biggest_cost[i] < big_result)
        {
            big_result = biggest_cost[i];
            position[1] = i;
        }

        if (lowest_cost[i] < low_result)
        {
            low_result = lowest_cost[i];
            position[2] = i;
        }
    }

    cout << "Srednio - wylosuj: " << position[0] << " ,wydasz okolo: " << avg_result << "\n";
    cout << "Najdrozej - wylosuj: " << position[1] << " ,wydasz okolo: " << big_result << "\n";
    cout << "Najtaniej - wylosuj: " << position[2] << " ,wydasz okolo: " << low_result << "\n";
    
}