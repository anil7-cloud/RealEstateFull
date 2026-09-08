#!/bin/bash

echo "======================="
echo " CODEBASE ANALYSIS"
echo "======================="

echo ""
echo "📊 TOTAL LINES:"
find . -name "*.cs" | xargs wc -l | tail -1

echo ""
echo "📁 FILE COUNT:"
find . -name "*.cs" | wc -l

echo ""
echo "🔥 BIGGEST FILES:"
find . -name "*.cs" -exec wc -l {} + | sort -n | tail -10

echo ""
echo "🧠 SERVICE COUNT:"
grep -r "class .*Service" . | wc -l

echo ""
echo "🗄️ DB USAGE:"
grep -r "DbContext" . | wc -l

echo ""
echo "⚠️ COUPLING RISK (new keyword):"
grep -r "new " . | wc -l

echo ""
echo "======================="
echo " DONE"
echo "======================="
