lmc.minimizeToTray = true

lmc_minimize()
-- ==========================================
-- 1. PRZYPISANIE URZĄDZENIA (Jeżeli nie działa, trzeba sprawdzić wypisane urządzenia i dopasować)
-- ==========================================
lmc_device_set_name('NUMPAD', 'VID_276D&PID_1010')
lmc_print_devices()

-- ==========================================
-- 2. STAŁE
-- ==========================================
-- Cyfry
local num_0 = 96
local num_1 = 97
local num_2 = 98
local num_3 = 99
local num_4 = 100
local num_5 = 101
local num_6 = 102
local num_7 = 103
local num_8 = 104
local num_9 = 105

-- Znaki specjalne
local sign_mult = 106 -- Gwiazdka (*)
local sign_plus  = 107 -- Plus (+)
local sign_minus  = 109 -- Minus (-)
local sign_dot  = 110 -- Kropka (.)1
local sign_div  = 111 -- Dzielenie (/) - przełącznik między trybami
local sign_ent  = 13  -- Enter
local sign_del  = 8   -- Backspace

-- Ignorowany numlock
local sign_numlock = 144 --wywalamy kolege, bo wysyła się przy każdym naciśnięciu klawisza

-- ==========================================
-- 3. USTAWIENIA TRYBÓW
-- ==========================================
local currentMode = 1
local modeNames = {
  [1] = "NORMAL", -- Do zwykłego używania klawiatury
  [2] = "DRAW", -- Pod rysowanie - głównie Krita/Animate
  [3] = "STREAM" -- Pod streaming - OBS
}

-- ==========================================
-- 4. NASTAWY KLAWISZY DLA TRYBÓW
-- ==========================================
local profiles = {

  -- === TRYB 1: NORMAL ===
  [1] = {
    [num_1]   = '1',
    [num_2]   = '2',
    [num_3]   = '3',
    [num_4]   = '4',
    [num_5]   = '5',
    [num_6]   = '6',
    [num_7]   = '7',
    [num_8]   = '8',
    [num_9]   = '9',
    [num_0]   = '0',
    [sign_minus] = '-',
    [sign_plus]  = '+',
    [sign_dot]  = '.',
    [sign_mult] = '*',
    [sign_ent] = '{ENTER}'
  },

  -- === TRYB 2: DRAW ===
  [2] = {
    [num_1]   = '^z',
    [num_2]   = '^c',
    [num_3]   = '^v',
    [num_4]   = '4', --rotacja w lewo
    [num_5]   = '5', --reset rotacji
    [num_6]   = '6', --rotacja w prawo
    [sign_ent] = '{ENTER}',
    [sign_minus] = 'e' --gumka
  },

  -- === TRYB 3: STREAM ===
  [3] = {
    -- na razie nie ruszam, potencjalne dodatki to: start/stop stream, zmiana sceny, mute/unmute mic
    [num_1]   = '1',
    [num_2]   = '2',
    [num_3]   = '3',
    [num_4]   = '4',
    [num_5]   = '5',
    [num_6]   = '6',
    [num_7]   = '7',
    [num_8]   = '8',
    [num_9]   = '9',
    [num_0]   = '0',
    [sign_minus] = '-',
    [sign_plus]  = '+',
    [sign_dot]  = '.',
    [sign_mult] = '*',
    [sign_ent] = '{ENTER}'
  }
}

-- ==========================================
-- 5. LOGIKA
-- ==========================================
lmc_set_handler('NUMPAD', function(button, direction)

  if (direction == 0) then return end
  if (button == sign_numlock) then return end

  if (button == sign_div) then --zmiana trybu
    currentMode = currentMode + 1
    if (currentMode > 3) then currentMode = 1 end

    print('>>> ZMIANA TRYBU NA: ' .. modeNames[currentMode])
    lmc_say(modeNames[currentMode])
    return
  end

  local modeConfig = profiles[currentMode]
  if (modeConfig) then
    local action = modeConfig[button]
    if (action) then
      lmc_send_keys(action)
      --print('Kliknięto kod: ' .. button) --pod testy
    end
  end

end)

lmc_minimize()
