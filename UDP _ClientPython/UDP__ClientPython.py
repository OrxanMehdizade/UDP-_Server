import socket
localIP="127.0.0.1"
localPort=27001
bufferSize=1024
msg= ''

UDPClientSocekt = socket.socket(family = socket.AF_INET, type = socket.SOCK_DGRAM)
UDPClientSocekt.bind((localIP,localPort))
print("UDP Client listening")

while True:
    bytesAddresPair= UDPClientSocekt.recvfrom(bufferSize)
    msg=bytesAddresPair[0]
    address=bytesAddresPair[1]
    print(f'{address} - {msg} ')


   

 


 

