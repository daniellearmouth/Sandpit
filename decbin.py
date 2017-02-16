import math
import re
import sys

def main():
    print("=== The Decimal Binary Conversion Wonder Machine ===")
    print("Daniel Learmouth - http://github.com/daniellearmouth")
    print("")
    print("This tool is designed to convert decimal and binary numbers.")
    print("Simply say whether you want to convert decimal to binary, or binary to decimal, ")
    print("then input a series of numbers, and out comes a converted number of your choice.")
    print("")
    start()

def start():
    selection = ''
    print("To convert decimal into binary, input 'D' or 'd'.")
    print("To convert binary into decimal, input 'B' or 'b'.")
    selection = input("Which conversion do you want to do? ")
    if selection not in ['B', 'b', 'D', 'd']:
        print("Sorry, I can only accept either upper-case or lower-case 'B' or 'D'. Restarting...")
        print("")
        start()
    elif selection in ['B', 'b']:
        print("Setting conversion from binary to decimal.")
        print("")
        bintodec()
    elif selection in ['D', 'd']:
        print("Setting conversion from decimal to binary.")
        print("")
        dectobin()

def bintodec():
    binaryinput = ''
    binarycon = []
    binaryjoin = ''
    binarystr = ''
    binaryint = 0
    binaryinput = input("Please input a sequence of ones and zeroes: ")
    if re.match('^[0-1]*$', binaryinput):
        if binaryinput[0] == '0':
            binaryinput = binaryinput[1:]
            binarycon.append('0b')
            binarycon.append(binaryinput)
        else:
            binarycon.append('0b')
            binarycon.append(binaryinput)
        binarystr = ''.join(binarycon)
        binaryint = int(binarystr, 2)
        print("{0} ==> {1}".format(binaryinput, str(binaryint)))
        restart()
    else:
        print("Sorry, you included characters besides 1 and 0. Restarting...")
        print("")
        bintodec()

def dectobin():
    decimalinput = ''
    decimalint = 0
    decimalcon = ''
    decimalinput = input("Please input a sequence of numbers (must be an integer!): ")
    if re.match('^[0-9]*$', decimalinput):
        decimalint = int(decimalinput)
        decimalcon = "{0:b}".format(decimalint)
        print("{0} ==> {1}".format(decimalinput, decimalcon))
        restart()
    else:
        print("Sorry, you entered characters that were not numbers. Restarting...")
        print("")
        dectobin()

def restart():
    selection = ''
    print("To do another conversion, input 'Y' or 'y'.")
    print("To terminate the program, input 'X' or 'x'.")
    selection = input("Which would you like to do?")
    if selection not in ['Y', 'y', 'X', 'x']:
        print("Sorry, I can only accept either upper-case or lower-case 'X' or 'Y'. Restarting...")
        print("")
        restart()
    elif selection in ['Y', 'y']:
        print("")
        start()
    elif selection in ['X', 'x']:
        print("Terminating Python interpreter. Have a very safe and productive day!")
        sys.exit()

main()
