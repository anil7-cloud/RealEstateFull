#!/bin/bash

echo "======================="
echo " DEPENDENCY MAP"
echo "======================="

echo ""
echo "🔗 USING GRAPH:"
grep -r "using REAL_ESTATE_CLEAN" .

echo ""
echo "⚠️ DIRECT NEW USAGE:"
grep -r "new " .

echo ""
echo "🧠 DB ACCESS:"
grep -r "DbContext" .

echo ""
echo "======================="
echo " DONE"
echo "======================="
