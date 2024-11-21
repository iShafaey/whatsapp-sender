import os
import time
import random
import pyfiglet
import re
from datetime import datetime
from selenium import webdriver
from selenium.common import NoSuchElementException
from selenium.webdriver.common.keys import Keys
from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import TimeoutException
from colorama import Fore, Style, init

title = "Whatsapp Sender by Ahmed Elshafie"
os.system(f'title {title}')

# Initialize colorama
init(autoreset=True)

def print_plus(type, message, message_color):
    type = (type or "SYSTEM")
    # total_length = 8
    # spaces = ' ' * (total_length - len(type))

    current_time = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    print(f"{Style.DIM + Style.BRIGHT + Fore.WHITE}[{current_time}]"
          f"[{Style.BRIGHT + Fore.YELLOW}{type.ljust(8)}{Style.RESET_ALL}] "
          f"{message_color}{message}{Style.RESET_ALL}")


# Create browser options
options = Options()

# Define the user-data path based on the operating system
user_data_path = os.path.join(os.getenv("LOCALAPPDATA"), "Google", "Chrome", "User Data")
options.add_experimental_option("excludeSwitches", ["enable-logging"])
options.add_argument("--timeout=60")
options.add_argument(f"--user-data-dir={user_data_path}")
options.add_argument("--profile-directory=Default")
options.add_argument("--remote-debugging-port=9222")
options.add_argument("--window-size=1200,800")
options.add_argument("--window-position=0,0")

# Init driver and welcome
wait_time = 20

blacklist_file = "blacklist.txt"
whitelist_file = "whitelist.txt"

ascii_art = pyfiglet.figlet_format(title)
print(ascii_art)
print_plus(type="WELCOME", message=f"{title}...", message_color=Style.DIM + Fore.CYAN)
print_plus(type="CONFIG", message=f"Driver: selenium", message_color=Style.DIM + Fore.CYAN)
print_plus(type="CONFIG", message=f"Browser: Chrome", message_color=Style.DIM + Fore.CYAN)
print_plus(type="CONFIG", message=f"Target service: Whatsapp", message_color=Style.DIM + Fore.CYAN)
print_plus(type="CONFIG", message=f"General Wait Time: {wait_time} sec", message_color=Style.DIM + Fore.CYAN)

time.sleep(1)

print_plus(type="CONFIG", message=f"Driver service registration...", message_color=Style.DIM + Fore.BLUE)

# Set up Chrome browser using webdriver_manager
service = Service(ChromeDriverManager().install())
driver = webdriver.Chrome(service=service, options=options)

print_plus(type="CONFIG", message=f"Driver service registered!", message_color=Style.DIM + Fore.GREEN)

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
            return filter_contacts(phone_numbers, blacklist_file)
    except FileNotFoundError:
        print_plus(type="SYSTEM", message=f"File '{filename}' not found.", message_color=Fore.RED)
        return []
    except UnicodeDecodeError:
        print_plus(type="SYSTEM", message=f"Error: Unable to decode the file. Please check the file encoding.", message_color=Fore.RED)
        return []

# Function to load message from a file with UTF-8 encoding
def load_message(filename="message.txt"):
    try:
        with open(filename, 'r', encoding='utf-8') as file:
            return file.read().strip()
    except FileNotFoundError:
        print_plus(type="SYSTEM", message=f"File '{filename}' not found.", message_color=Fore.RED)
        return ""

# Function to get all images from the images folder
def get_all_images(folder="images"):
    try:
        images = [os.path.join(folder, img) for img in os.listdir(folder) if
                  img.lower().endswith(('.png', '.jpg', '.jpeg', '.gif'))]
        if images:
            return images
        else:
            print_plus(type="SYSTEM", message=f"No images found in the 'images' folder.", message_color=Fore.YELLOW)
            return []
    except FileNotFoundError:
        print_plus(type="SYSTEM", message=f"Folder '{folder}' not found.", message_color=Fore.RED)
        return []

# Read blacklist file
def load_blacklist(file_path):
    if not os.path.exists(file_path):
        return set()
    with open(file_path, "r", encoding="utf-8") as f:
        return set(line.strip() for line in f if line.strip())

# Check if the phone number is shorter than 9 digits and add to blacklist.
def is_short_number(phone_number):
    # Ensure the blacklist file exists
    if not os.path.exists(blacklist_file):
        with open(blacklist_file, "w") as f:
            pass  # Create the file if it doesn't exist

    # Read the blacklist to check if the number is already in it
    with open(blacklist_file, "r") as f:
        blacklist = f.read().splitlines()

    # If the number is already in the blacklist, do nothing
    if phone_number in blacklist:
        print_plus(type="SYSTEM", message=f"Number {phone_number} is already in the blacklist.", message_color=Fore.RED)
        return False  # The number is already in the blacklist

    # If the number is short, add it to the blacklist
    if len(phone_number) < 9:
        with open(blacklist_file, "a") as f:
            f.write(phone_number + "\n")
        print_plus(type="SYSTEM", message=f"Number {phone_number} is too short and has been added to the blacklist.", message_color=Fore.RED)
        return True  # The number was added to the blacklist

    return False  # The number is not short, so nothing was done

# Check if the phone number is in the blacklist file
def check_blacklist(phone_number):
    # Ensure the blacklist file exists
    if not os.path.exists(blacklist_file):
        with open(blacklist_file, "w") as f:
            pass  # Create the file if it doesn't exist

    # Read the blacklist
    with open(blacklist_file, "r") as f:
        blacklist = f.read().splitlines()

    # Check if the number is in the blacklist
    if phone_number in blacklist:
        print_plus(type="SYSTEM", message=f"Number {phone_number} is already in the blacklist.", message_color=Fore.RED)
        return True

    # Add the number to the blacklist
    with open(blacklist_file, "a") as f:
        f.write(phone_number + "\n")
    print_plus(type="SYSTEM", message=f"Number {phone_number} has been added to the blacklist.", message_color=Fore.RED)
    return False

def check_number_at_blacklist(number):
    with open(blacklist_file, 'r') as file:
        blacklist = file.read().splitlines()

    if number in blacklist:
        return True
    else:
        return False

def check_whitelist(phone_number):
    # Ensure the whitelist file exists
    if not os.path.exists(whitelist_file):
        with open(whitelist_file, "w") as f:
            pass  # Create the file if it doesn't exist

    # Read the whitelist
    with open(whitelist_file, "r") as f:
        whitelist = f.read().splitlines()

    # Check if the number is in the whitelist
    if phone_number in whitelist:
        print_plus(type="SYSTEM", message=f"Number {phone_number} is already in the whitelist.", message_color=Fore.GREEN)
        return True

    # Add the number to the whitelist
    with open(whitelist_file, "a") as f:
        f.write(phone_number + "\n")
    print_plus(type="SYSTEM", message=f"Number {phone_number} has been added to the whitelist.", message_color=Fore.GREEN)
    return False

def check_number_at_whitelist(number):
    with open(whitelist_file, 'r') as file:
        whitelist = file.read().splitlines()

    if number in whitelist:
        return True
    else:
        return False

# Filter contacts
def filter_contacts(contacts, blacklist):
    try:
        with open(blacklist_file, 'r') as blacklist:
            blacklist_numbers = set(line.strip() for line in blacklist)

        filtered_contacts = [contact for contact in contacts if contact not in blacklist_numbers]

        return filtered_contacts

    except Exception as e:
        print_plus(type="SYSTEM", message=f"Failed to filter contacts: {type(e).__name__}", message_color=Fore.RED)
        return []

# Check whatsapp number
def check_whatsapp_number(driver, phone_number, message):
    if check_number_at_whitelist(phone_number): return True

    print_plus(type="SYSTEM", message=f"Checking Number {phone_number} is registered on WhatsApp or not...", message_color=Fore.CYAN)

    try:
        # Open the WhatsApp Web URL with the phone number
        whatsapp_url = f"https://web.whatsapp.com/send?phone={phone_number}&text="
        driver.get(whatsapp_url)

        try:
            # Check for error message indicating the number is not on WhatsApp
            wait = WebDriverWait(driver, wait_time)
            error_message = wait.until(EC.presence_of_element_located((By.XPATH, '//div[contains(text(), "Phone number shared via url is invalid.")]')))

            if error_message:
                print_plus(type="WHATSAPP", message=f"Number {phone_number} is not registered on WhatsApp.", message_color=Fore.RED)
                print_plus(type="WHATSAPP", message=error_message.text, message_color=Fore.RED)

                # Add the number to the blacklist
                check_blacklist(phone_number)
                return False
            else:
                print_plus(type="WHATSAPP", message=f"Number {phone_number} is registered on WhatsApp.", message_color=Fore.GREEN)
                check_whitelist(phone_number)
                return True

        except TimeoutException:  # Handle case where error message is not found within wait time
            print_plus(type="WHATSAPP", message=f"Number {phone_number} is registered on WhatsApp.", message_color=Fore.GREEN)
            check_whitelist(phone_number)
            return True

    except Exception as e:
        print_plus(type="DEBUG", message=f"An error occurred: {type(e).__name__}", message_color=Fore.RED)
        return False

def send_whatsapp_message(phone_number, message, images):
    print_plus(type="WHATSAPP", message=f"Started sending message to {phone_number}", message_color=Fore.CYAN)
    try:
        whatsapp_url = f"https://web.whatsapp.com/send?phone={phone_number}&text="
        driver.get(whatsapp_url)

        if check_whatsapp_number(driver, phone_number, message) == False:
            return False


        driver.set_window_position(10000, 10000)
        driver.set_window_size(800, 800)

        # Check if there are images to attach
        if images:
            wait = WebDriverWait(driver, wait_time)
            attach_files_button = wait.until(EC.element_to_be_clickable((By.CSS_SELECTOR, 'span[data-icon="plus"]')))
            attach_files_button.click()

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
                        print_plus(type="SYSTEM", message=f"Neither XPath was found: {type(e).__name__}", message_color=Fore.RED)

                file_input.send_keys(absolute_image_path)
                
            # Send the message text after attaching images
            wait = WebDriverWait(driver, wait_time)
            attach_files_button = wait.until(EC.element_to_be_clickable((By.CSS_SELECTOR, 'span[data-icon="send"]')))
            attach_files_button.click()

            # time.sleep(0.2)
            # driver.minimize_window()

        # Send the message text
        time.sleep(5)
        wait = WebDriverWait(driver, wait_time)
        message_box = wait.until(EC.element_to_be_clickable((By.XPATH, '//*[@id="main"]/footer/div[1]/div/span/div/div[2]/div[1]/div/div[1]/p')))
        message_box.send_keys(message)

        wait = WebDriverWait(driver, wait_time)
        send_button = wait.until(EC.element_to_be_clickable((By.XPATH, '//*[@id="main"]/footer/div[1]/div/span/div/div[2]/div[2]/button')))
        send_button.click()    
        return True

    except Exception as e:
        print_plus(type="SYSTEM", message=f"Failed to send message: {type(e).__name__}", message_color=Fore.RED)
        return False


# Main script to send messages and track the results
def main():
    phone_numbers = load_phone_numbers()
    message = load_message()
    images = get_all_images()

    if not phone_numbers:
        print_plus(type="SYSTEM", message=f"No phone numbers available.", message_color=Fore.RED)
        return
    if not message:
        print_plus(type="SYSTEM", message=f"No message content available.", message_color=Fore.RED)
        return

    total_contacts = len(phone_numbers)
    sent_count = 0

    # Send messages to each phone number
    for phone_number in phone_numbers:
        if check_number_at_blacklist(phone_number):
            print_plus(type="SYSTEM", message=f"Number {phone_number} is already in the blacklist.", message_color=Fore.RED)
            continue
        
        if send_whatsapp_message(phone_number, message, images):
            sent_count += 1
            print_plus(type="SYSTEM", message=f"Message sent to {phone_number}", message_color=Fore.GREEN)
        else:
            print_plus(type="SYSTEM", message=f"Failed to send message to {phone_number}", message_color=Fore.RED)

        # Add a random wait time between sending to different contacts
        wait_time = random.randint(15, 35)
        print_plus(type="SYSTEM", message=f"Waiting for {wait_time} seconds before sending to the next number...", message_color=Fore.YELLOW)
        time.sleep(wait_time)

    # Display final message based on the results
    if sent_count == total_contacts:
        print_plus(type="SYSTEM", message=f"All messages sent successfully! Sent {sent_count} out of {total_contacts}.", message_color=Fore.GREEN)
    else:
        print_plus(type="SYSTEM", message=f"Finished with partial success: Sent {sent_count} out of {total_contacts}.", message_color=Fore.YELLOW)

    # Prompt the user to press enter key to exit
    input(Fore.CYAN + "Press ENTER key to exit...")


# Run the main script
main()
