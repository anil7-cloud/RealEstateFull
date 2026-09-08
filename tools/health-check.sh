#!/bin/bash

FILES=$(find . -name "*.cs" | wc -l)
LINES=$(find . -name "*.cs" | xargs wc -l | tail -1 | awk '{print $1}')
SERVICES=$(grep -r "Service" . | wc -l)

SCORE=$((100 - (FILES/5 + LINES/200 + SERVICES)))

echo "=========================="
echo " SAAS HEALTH CHECK"
echo "=========================="
echo "Files: $FILES"
echo "Lines: $LINES"
echo "Services: $SERVICES"
echo "Score: $SCORE / 100"
echo "=========================="
