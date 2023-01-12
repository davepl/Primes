# For more information, please refer to https://aka.ms/vscode-docker-python
FROM python:3.9-slim-buster

# Install pip requirements
RUN python -m pip install numba
RUN python -m pip install numpy

WORKDIR /opt/app
COPY PrimePY.py .

# During debugging, this entry point will be overridden. For more information, please refer to https://aka.ms/vscode-docker-python-debug
ENTRYPOINT ["python3", "PrimePY.py"]