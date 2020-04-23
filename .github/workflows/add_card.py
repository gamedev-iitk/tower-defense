import requests
import sys

url = "https://api.github.com/repos/gamedev-iitk/tower-defense/issues"
response = requests.request("GET",url)
response= response.json()
last_issue = response[0]

name = last_issue["title"] 
desc = last_issue["body"]
url = "https://api.trello.com/1/cards"
key = sys.argv[1]
token = sys.argv[2]

query = {
   'name':name,
   'desc':desc,
   'idList':'5e8f29ddf7723735bea6e342',
   'key':key,
   'token':token
}

response = requests.request(
   "POST",
   url,
   params=query
)
