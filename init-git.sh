#!/usr/bin/env bash

# Exit immediately if a command exits with a non-zero status
set -e

echo "Initializing Git repository..."
git init

echo "Adding all files..."
git add .

echo "Creating initial commit..."
git commit -m "Initial commit: Setup Clean Architecture"

echo "Switching to main branch..."
git branch -M main

echo "Creating GitHub repository and pushing code..."
# Uses GitHub CLI to create a private repo named "TakweneMusic" using the current folder as source,
# configures the 'origin' remote, and pushes the commits.
gh repo create TakweneMusic --private --source=. --remote=origin --push

echo "Successfully setup Git and pushed to GitHub!"
