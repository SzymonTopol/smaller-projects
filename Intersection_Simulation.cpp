#include <Arduino.h>

//Prosty projekt do symulacji działania sygnalizacji świetlnej na skrzyżowaniu

#define MAIN_GREEN 13
#define MAIN_YELLOW 14
#define MAIN_RED 27
#define MAIN_WALK 5
#define MAIN_BUTTON 32

#define CROSS_GREEN 21
#define CROSS_YELLOW 22
#define CROSS_RED 23
#define CROSS_WALK 18
#define CROSS_BUTTON 33

//FLAGS
bool main_button_pressed = false;
bool cross_button_pressed = false;

#define MAIN_GO 6000  // 2
#define MAIN_STOPPING 3000 // 3
#define ALL_STOP 3000 // 0, 4
#define CROSS_GET_READY 3000 // 5
#define CROSS_GO 6000 // 6
#define CROSS_STOPPING 3000 // 7
#define MAIN_GET_READY 3000 // 1

unsigned long compare_previous_time = 0;
unsigned long state_duration[8] = {ALL_STOP, MAIN_GET_READY, MAIN_GO, MAIN_STOPPING, ALL_STOP, CROSS_GET_READY, CROSS_GO, CROSS_STOPPING};
unsigned short CURRENT_STATE = 0;
unsigned short MAX_STATE = 7;

void change_main_lights(short red_state, short yellow_state, short green_state) {
  digitalWrite(MAIN_RED, red_state);
  digitalWrite(MAIN_YELLOW, yellow_state);
  digitalWrite(MAIN_GREEN, green_state);
}

void change_cross_lights(short red_state, short yellow_state, short green_state) {
  digitalWrite(CROSS_RED, red_state);
  digitalWrite(CROSS_YELLOW, yellow_state);
  digitalWrite(CROSS_GREEN, green_state);
}

void setup() {
  Serial.begin(115200);

  Serial.println("Traffic light system starting...");

  pinMode(MAIN_GREEN, OUTPUT);
  pinMode(MAIN_YELLOW, OUTPUT);
  pinMode(MAIN_RED, OUTPUT);
  pinMode(MAIN_WALK, OUTPUT);
  pinMode(MAIN_BUTTON, INPUT_PULLUP);

  pinMode(CROSS_GREEN, OUTPUT);
  pinMode(CROSS_YELLOW, OUTPUT);
  pinMode(CROSS_RED, OUTPUT);
  pinMode(CROSS_WALK, OUTPUT);
  pinMode(CROSS_BUTTON, INPUT_PULLUP);
}

void loop() {
  if (millis()-compare_previous_time>state_duration[CURRENT_STATE])
  {
    compare_previous_time = millis();
    CURRENT_STATE == MAX_STATE? CURRENT_STATE = 0 : CURRENT_STATE++;

    Serial.println(String(CURRENT_STATE));
  }

  if(digitalRead(MAIN_BUTTON) == LOW) main_button_pressed = true;
  if(digitalRead(CROSS_BUTTON) == LOW) cross_button_pressed = true;

  switch (CURRENT_STATE)
  {
  case 0: //ALL_STOP
  change_main_lights(HIGH,LOW,LOW);
  change_cross_lights(HIGH,LOW,LOW);
    break;
  case 1: //MAIN_GET_READY
  change_main_lights(HIGH,HIGH,LOW);
  change_cross_lights(HIGH,LOW,LOW);
    break;
  case 2: //MAIN_GO
  change_main_lights(LOW,LOW,HIGH);
  change_cross_lights(HIGH,LOW,LOW);
  if (cross_button_pressed)
  {
    cross_button_pressed = false;
    digitalWrite(CROSS_WALK, HIGH);
  }
    break;
  case 3: //MAIN_STOPPING
  change_main_lights(LOW,HIGH,LOW);
  change_cross_lights(HIGH,LOW,LOW);
  digitalWrite(CROSS_WALK, LOW);
    break;
  case 4: //ALL_STOP
  change_main_lights(HIGH,LOW,LOW);
  change_cross_lights(HIGH,LOW,LOW);
    break;
  case 5: //CROSS_GET_READY
  change_main_lights(HIGH,LOW,LOW);
  change_cross_lights(HIGH, HIGH, LOW);
    break;
  case 6: //CROSS_GO
  change_main_lights(HIGH,LOW,LOW);
  change_cross_lights(LOW, LOW, HIGH);
  if (main_button_pressed)
  {
    main_button_pressed = false;
    digitalWrite(MAIN_WALK, HIGH);
  }
    break;
  case 7: //CROSS_STOPPING
  change_main_lights(HIGH,LOW,LOW);
  change_cross_lights(LOW, HIGH, LOW);
  digitalWrite(MAIN_WALK, LOW);    
  break;
  default:
    break;
  }
  
}
