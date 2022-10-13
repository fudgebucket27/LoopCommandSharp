# Intro
LoopCommandSharp is a cross platform toolkit for everything Loopring related!

# Setup
Grab a release for your platform from here: https://github.com/fudgebucket27/LoopCommandSharp/releases . 

Double click on the executable to run. You might have to allow additional security settings depending on your platform. 

mac users may need to run this command in a terminal window in the same folder as the unzipped LoopCommandSharp in order to run.
```bash
chmod +x LoopCommandSharp
```

You will notice some of the buttons are greyed out initially, you will need to click on the "Settings" button and export out your loopring api keys from loopring.io and etc into this window and click on save. 

Your main window should now look like this:

![image](https://user-images.githubusercontent.com/5258063/195521543-b6d21a09-2ff1-4b70-b9df-0a0a710aa528.png)

There are two functions currently
* Create NFT Collection
* Mint NFT

## Create NFT collection
This window allows you to create a new collection on Loopring. The window should look like below:

![image](https://user-images.githubusercontent.com/5258063/195522012-14da3a5c-6199-4d21-8aa0-56c6ad042e51.png)

* Collection Name: Needs to be a unique name for the collection
* Collection Description: Description of your collection
* Collection Avatar: An image link on ipfs or http
* Collection Banner: An image link on ipfs or http
* Collection TileUri: An image link on ipfs or http

## Mint NFT
This window allows you to mint NFTs into a collection on Loopring. The window should look like below:

![image](https://user-images.githubusercontent.com/5258063/195522545-f1083d95-3009-4086-b41f-9cbb8fbc35e2.png)

* Collection: Choose a collection from the dropdown to mint into
* Royalty Percentage: A whole number between 0 and 10 to dictate the royalty percentage
* Editions Per Mint: The editions per mint. Leave to 1 for 1/1 mints.
* Royalty Address: The address to assign royalties to. Defaults to the loopring address setup in the settings window.
* Enter IPFS JSON CIDS here per line text box: Enter an ipfs json cidv0(begins with Qm) per line in this text box.


