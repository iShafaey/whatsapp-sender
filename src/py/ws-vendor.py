import os
import re
import time
import random
from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.options import Options
from colorama import Fore, init
from selenium.webdriver.common.action_chains import ActionChains

# Initialize colorama
init(autoreset=True)

# Create browser options
options = Options()
user_data_path = os.path.join(os.getenv("LOCALAPPDATA"), "Google", "Chrome", "User Data")
options.add_experimental_option("excludeSwitches", ["enable-logging"])
options.add_argument("--timeout=60")
options.add_argument(f"--user-data-dir={user_data_path}")
options.add_argument("--profile-directory=Default1")
options.add_argument("--remote-debugging-port=9222")
options.add_argument("--window-size=1200,800")
options.add_argument("--window-position=0,0")

# Set up Chrome browser using webdriver_manager
service = Service(ChromeDriverManager().install())
driver = webdriver.Chrome(service=service, options=options)


# Function to load phone numbers from a file
def load_phone_numbers(filename="numbers.txt"):
    try:
        with open(filename, 'r', encoding='utf-8') as file:
            phone_numbers = []
            for line in file:
                match = re.search(r',\s*(\+?\d+)', line.strip())
                if match:
                    phone_number = match.group(1)
                    if not phone_number.startswith('+20'):
                        phone_number = '+20' + phone_number.lstrip('0')
                    phone_numbers.append(phone_number)
            return phone_numbers
    except FileNotFoundError:
        print(Fore.RED + f"File '{filename}' not found.")
        return []
    except UnicodeDecodeError:
        print(Fore.RED + f"Error: Unable to decode the file. Please check the file encoding.")
        return []


# Function to load message from a file with UTF-8 encoding
def load_message(filename="message.txt"):
    try:
        with open(filename, 'r', encoding='utf-8') as file:
            return file.read().strip()
    except FileNotFoundError:
        print(Fore.RED + f"File '{filename}' not found.")
        return ""


# Function to get all images from the images folder
def get_all_images(folder="images"):
    try:
        images = [os.path.join(folder, img) for img in os.listdir(folder) if
                  img.lower().endswith(('.png', '.jpg', '.jpeg', '.gif'))]
        if images:
            return images
        else:
            print(Fore.YELLOW + "No images found in the 'images' folder.")
            return []
    except FileNotFoundError:
        print(Fore.RED + f"Folder '{folder}' not found.")
        return []


def send_whatsapp_message(phone_number, message, images):
    print(Fore.CYAN + f"Started sending message to {phone_number}")
    try:
        whatsapp_url = f"https://web.whatsapp.com/send?phone={phone_number}"
        driver.get(whatsapp_url)
        time.sleep(20)  # Wait for WhatsApp Web to load

        # Check if there are images to attach
        if images:
            attach_files_button = driver.find_element(By.CSS_SELECTOR, 'span[data-icon="plus"]')
            attach_files_button.click()
            time.sleep(1)

            # Iterate over each image and attach it
            for index, image_path in enumerate(images):
                # Convert relative path to absolute path
                absolute_image_path = os.path.abspath(image_path)

                try:
                    file_input = driver.find_element(By.XPATH, '//input[@accept="image/*,video/mp4,video/3gpp,video/quicktime"]')
                except:
                    try:
                        file_input = driver.find_element(By.XPATH, '//input[@accept="*"]')
                    except Exception as e:
                        print(f"Neither XPath was found: {type(e).__name__}")

                file_input.send_keys(absolute_image_path)
                time.sleep(5)  # Wait for the image to load in WhatsApp before attaching another one

            # Send the message text after attaching images
            message_box = driver.find_element(By.XPATH, '//*[@id="app"]/div/div[3]/div[2]/div[2]/span/div/div/div/div[2]/div/div[1]/div[3]/div/div/div[2]/div[1]/div[1]/p')
            message_box.send_keys(message)
            message_box.send_keys(Keys.ENTER)

            driver.set_window_position(10000, 10000)
            driver.set_window_size(800, 800)
            # time.sleep(0.2)
            # driver.minimize_window()

            time.sleep(5)
        else:
            # Send the message text when not images
            message_box = driver.find_element(By.XPATH, '//*[@id="main"]/footer/div[1]/div/span/div/div[2]/div[1]/div/div[1]/p')
            message_box.send_keys(message)
            message_box.send_keys(Keys.ENTER)

            time.sleep(5)

        return True

    except Exception as e:
        print(Fore.RED + f"Failed to send message: {type(e).__name__}")
        return False


# Main script to send messages and track the results
def main():
    phone_numbers = load_phone_numbers()
    message = load_message()
    images = get_all_images()

    if not phone_numbers:
        print(Fore.RED + "No phone numbers available.")
        return
    if not message:
        print(Fore.RED + "No message content available.")
        return

    total_contacts = len(phone_numbers)
    sent_count = 0

    # Send messages to each phone number
    for phone_number in phone_numbers:
        if send_whatsapp_message(phone_number, message, images):
            sent_count += 1
            print(Fore.GREEN + f"Message sent to {phone_number}")
        else:
            print(Fore.RED + f"Failed to send message to {phone_number}")

        # Add a random wait time between sending to different contacts
        wait_time = random.randint(30, 60)
        print(Fore.YELLOW + f"Waiting for {wait_time} seconds before sending to the next number...")
        time.sleep(wait_time)

    # Display final message based on the results
    if sent_count == total_contacts:
        print(Fore.GREEN + f"All messages sent successfully! Sent {sent_count} out of {total_contacts}.")
    else:
        print(Fore.YELLOW + f"Finished with partial success: Sent {sent_count} out of {total_contacts}.")

    # Prompt the user to press enter key to exit
    input(Fore.CYAN + "Press ENTER key to exit...")


# Run the main script
main()
