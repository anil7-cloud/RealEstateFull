#!/bin/bash

echo "=========================="
echo " FULL SAAS ANALYZER"
echo "=========================="

echo ""
echo "📁 FILES:"
find . -name "*.cs" | wc -l

echo ""
echo "📊 LINES:"
find . -name "*.cs" | xargs wc -l | tail -1

echo ""
echo "⚙️ SERVICES:"
grep -r "Service" . | wc -l

echo ""
echo "🌐 API:"
grep -r "MapGet\|MapPost" . | wc -l

echo ""
echo "🧠 DB:"
grep -r "DbContext" . | wc -l

echo ""
echo "🔥 BIG FILES:"
find . -name "*.cs" -exec wc -l {} + | awk '$1 > 150'

echo ""
echo "=========================="
echo " DONE"
echo "=========================="
