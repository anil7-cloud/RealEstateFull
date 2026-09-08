#!/bin/bash

echo "============================"
echo " AI ARCHITECT ANALYZER"
echo "============================"

echo ""
echo "📊 TOTAL LINES:"
find . -name "*.cs" | xargs wc -l | tail -1

echo ""
echo "📁 FILE COUNT:"
find . -name "*.cs" | wc -l

echo ""
echo "⚙️ SERVICES:"
grep -r "class .*Service" . | wc -l

echo ""
echo "🧠 API ENDPOINTS:"
grep -r "MapPost\|MapGet" . | wc -l

echo ""
echo "⚠️ DB CONTEXT:"
grep -r "DbContext" . | wc -l

echo ""
echo "🔥 LARGE FILES:"
find . -name "*.cs" -exec wc -l {} + | awk '$1 > 150'

echo ""
echo "💣 COUPLING (new keyword):"
grep -r "new " . | wc -l

echo ""
echo "============================"
echo " DONE"
echo "============================"
