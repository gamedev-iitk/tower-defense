import requests
import issues
import sys

name = issues.titles
desc = issues.body
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

