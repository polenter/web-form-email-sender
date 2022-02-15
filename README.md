# web-form-email-sender
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
