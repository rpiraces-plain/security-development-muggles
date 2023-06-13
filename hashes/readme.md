# Hashes, salt and deprecated algorithms

In this directory you will find a basic example of hashing: first, a MD5, and then SHA-256, both with and without salting.

We will be testing this hashes using predictable passwords. To do so, we will check with [crackstation](https://crackstation.net/) what are the benefits of using salt, and why **chosing a strong key/password is vital**.

Finally, in order to test this code, we will be using Jupyter Notebooks with Docker. Attached in the `./src` folder there is a notebook that we can load and execute with Jupyter.

## Setup
Just run `docker container run -p 8888:8888 jupyter/demo` and open your favourite browser with the link that the terminal is going to give you: `http://localhost:8888/?token=123....`

Once inside, **upload** the Hashes_salt.ipynb file from the browser and open it.