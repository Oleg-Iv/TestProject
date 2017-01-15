# TestProject
Here is the detail of the assignment:

Please develop a webform or MVC web app that has 3 users: MasterUser, User1 and User2 (store those info on SQL or a config file) and three pages: 1- login page 2- welcome page 3- MasterUser's Page

1- User MUST login to the web app. Then the app creates a session for each authentication then redirects the user to welcome page

2- MasterUser should be able to log out other users remotely (User1 or User2) by clicking a button. When that happens that  user's session should get expired and the user should 'immediately' get redirected to login page

3- [plus] Put 3 TextBoxes and a Button on the welcome page. When user clicks on the button, send a request to server to get a new randomly generated AES256 key from the server. then encrypt (with the key you just received from the server) the content of TextBox1 and put it on TextBox2. then send the encrypted content to server. On the server side decrypt the content and send it back to the client after 5 second delay. then on the client side, put the decrypted content on text box 3. 

4- Use RSA2048 for encryption the AES key. Every time you send a request from frontend to backend for getting a new AES key, I generate a new pair of keys (public and private) and send the public key to server , server uses the public key to encrypt the message(AES key) and in frontend you use the private key to decrypt the message and retrieve the AES key.
