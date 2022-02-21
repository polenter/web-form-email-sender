# web-form-email-sender v.0.11
Sends email after submitting a web form.


## Build

    docker build \
    -t web-form-email-sender \
    --build-arg USERNAME=stefan \
    --build-arg UID=10000 \
    --build-arg GID=10000 \
    .


## Run

    docker run -d --rm -p 8090:5000 web-form-email-sender


## Web Form

### Required fields
Must contain the following hidden fields.  
* `ClientId` - identifies the HTML form/client.
* `SenderEmail` - is used as `reply to`.


### Optional fields
* `Text` - message text


### Honey Pot
* `Subject` - a hidden field, used for honey pot, must be empty, otherwise the email will not be sent and no error will be reported.



## Configuration

### FormClients

        "FormClients": [
        {
            "ClientId": "b37b04b7785bf3a",
            "TargetEmail": "support@example.com",
            "Website":  "www.example.com" 
        }
        ]

A client must be defined, otherwise the email will no be sent to the client.

`ClientId` must be set on the html form as a hidden text field `clientid`.  
`TargetEmail` - is used as the `to` field
`Website` is contained in the message subject.


### SmtpEmailSender

        "SmtpEmailSender": {
        "Host": "smtp.example.com",
        "Port": 587,
        "Username": "noreply@example.com",
        "Password": "noreply123",
        "From": ""
        }

There is only STARTTLS supported (default port 587).  
Is there no `From` defined, the `Username` is used as the `from` field in the email.


### KnownCorsOrigins

        "KnownCorsOrigins": [
        "http://localhost:3000"
        ]

Used if Javascript is used for sending the html form.