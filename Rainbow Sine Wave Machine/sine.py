import math
import sys

def main():
  print("\033[1mThe Rainbow Sine Wave Machine by Daniel Learmouth - http://daniellearmouth.com")
  print("\033[1m==============================================================================")
  print("\033[39;22mThis little app is only one hundred lines of code long, if you're curious.")
  print("All you have to do is type in a phrase and say whether or not you want the text to be in colour or black and white.")
  print("The result will be a sinusoidal wave with your text, colour of choice, and an additive character before it to make the wave.")
  print("Do note, however, that this app is intended to run in Windows' command prompt, and requires Python 3.5 to be installed.")
  print("\033[1m=======================================================================================================================")
  start()

def start():
  colset=''
  f=''
  t=0.0
  c=1
  colset = input("\033[39;22mWould you like the wave to be in rainbow colours? 'No' will return white. Y or N. ")
  if colset not in ['Y', 'y', 'N', 'n']:
    print("Sorry, I don't understand the command '"+colset+"'.")
    start()
  else:
    phrase = input("Please type a phrase. The wave will be created from this. ")
    lines = input("How many lines would you like the wave to take? Whole numbers only. ")
    wave = input("What frequency would you like the wave at? Numbers between 0.25 and 0.75 are recommended. ")
    depth = input("How big an amplitude do you want the wave to have? Whole numbers only. ")
    offset = input("How much of an offset would you like to add to the wave? Whole numbers only. ")
    additive = input("Please select a character that will create the wave. Only one character. ")

  try:
    iLines = int(lines)
    fWave = float(wave)
    iDepth = int(depth)
    iOffset = int(offset)
  except ValueError:
  	print("Sorry. It appears that there are characters besides numbers and decimal points in variables where that's not allowed.")
  	print("Please only use numbers for where it asks for a number, and a decimal point for frequency if applicable.")
  	print("Restarting......")
  	print("")
  	start()
  finally:
    iLines = int(lines)
    fWave = float(wave)
    iDepth = int(depth)
    iOffset = int(offset)
    print("")
    print("Colour?: "+colset+" // Phrase: "+phrase+" // Number of Lines: "+lines+" // Wavelength: "+wave)
    print("Amplitude: "+depth+" // Offset: "+offset+" // Additive character: "+additive)
    conkey = input("\033[1mAre these selections OK? Any key other than Y will default to a restart. ")
    if conkey in ['Y', 'y']:
      for x in range(0,iLines):
        t += fWave
        s = " "
        v = int(round(math.sin(t)*iDepth))+iDepth+iOffset
        for k in range(0,v):
          s += additive
        if colset in ['Y', 'y']:
          if c == 1:
            print("\033[31;1m"+s+phrase)
            c += 1
          elif c == 2:
            print("\033[33;1m"+s+phrase)
            c += 1
          elif c == 3:
            print("\033[32;1m"+s+phrase)
            c += 1
          elif c == 4:
            print("\033[36;1m"+s+phrase)
            c += 1
          elif c == 5:
            print("\033[34;1m"+s+phrase)
            c += 1
          elif c == 6:
            print("\033[35;1m"+s+phrase)
            c += 1
          else:
            c = 1
            x += 1
        elif colset in ['N', 'n']:
          print("\033[37;1m"+s+phrase)
    else:
      start()
  print("\033[39;22mComplete!")
  again()

def again():
  option = ""
  option = input("Do you want another one? Y or N. ")
  if option in ['Y', 'y']:
    start()
  elif option in ['N', 'n']:
    print("OK then. Thank you for checking this out!")
    print("Don't forget to check my website for more stuff! http://daniellearmouth.com")
    sys.exit(0)
  else:
    print("Sorry, I don't understand the command '"+option+"'.")
  again()

main()