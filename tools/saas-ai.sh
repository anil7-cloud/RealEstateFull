#!/bin/bash

echo "===================================="
echo "  SAAS AI ARCHITECT ENGINE V1"
echo "===================================="

echo ""
echo "📁 PROJECT OVERVIEW"

FILES=$(find . -name "*.cs" | wc -l)
LINES=$(find . -name "*.cs" | xargs wc -l | tail -1 | awk '{print $1}')
SERVICES=$(grep -r "class .*Service" . | wc -l)
ENDPOINTS=$(grep -r "MapGet\|MapPost" . | wc -l)
DB=$(grep -r "DbContext" . | wc -l)
NEWUSAGE=$(grep -r "new " . | wc -l)

echo "Files: $FILES"
echo "Lines: $LINES"
echo "Services: $SERVICES"
echo "Endpoints: $ENDPOINTS"
echo "DbContext usage: $DB"
echo "New keyword usage: $NEWUSAGE"

echo ""
echo "🔥 BIG FILES (>150 lines):"
find . -name "*.cs" -exec wc -l {} + | awk '$1 > 150'

echo ""
echo "⚠️ ARCHITECTURE RISKS:"
if [ $NEWUSAGE -gt 50 ]; then
  echo "- HIGH COUPLING detected"
fi

if [ $DB -gt 10 ]; then
  echo "- DB overuse risk"
fi

if [ $SERVICES -gt 20 ]; then
  echo "- Service explosion risk"
fi

echo ""
echo "🧠 HEALTH SCORE CALCULATION"

SCORE=$((100 - (FILES/5 + LINES/200 + SERVICES + NEWUSAGE/10)))

echo "SAAS HEALTH SCORE: $SCORE / 100"

if [ $SCORE -gt 80 ]; then
  echo "STATUS: 🟢 GOOD"
elif [ $SCORE -gt 50 ]; then
  echo "STATUS: 🟡 MEDIUM"
else
  echo "STATUS: 🔴 NEED REFACTOR"
fi

echo ""
echo "===================================="
echo " DONE"
echo "===================================="
