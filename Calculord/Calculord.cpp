// Calculord.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Calculord.h"
#include <fstream>

double calculord::rxcalc::simpleParse(std::string str) const
{
	std::smatch simpleParseResult;

	if (regex_search(str, simpleParseResult, _simplePattern))
	{
		auto a = stod(simpleParseResult.str(1));
		auto b = stod(simpleParseResult.str(3));

		switch (simpleParseResult.str(2)[0])
		{
		case '+': return a + b;
		case '-': return a - b;
		case '*': return a * b;
		case '/':
			{
				if (b != 0)
				{
					return a / b;
				}
				throw dnoe;
			}
		default: throw;
		}
	}
	throw;
}

calculord::rxcalc::rxcalc():
	_simplePattern("(\\d*[.]?\\d+)([\\+\\-\\/\\*])?(\\d*[.]?\\d+)")
{
	_patterns.emplace_back("(\\([^)]+\\))"); // brackets
	_patterns.emplace_back("(\\d*[.]?\\d+)([\\*\\/])(\\d*[.]?\\d+)"); //first_priority
	_patterns.emplace_back("(\\d*[.]?\\d+)([\\+\\-])(\\d*[.]?\\d+)"); //second_priority
}

double calculord::rxcalc::calculate(std::string expression) const
{
	// erase spaces
	expression.erase(remove_if(expression.begin(), expression.end(), isspace), expression.end());

	std::smatch regexResult;

	for (auto&& pattern : _patterns)
	{
		while (regex_search(expression, regexResult, pattern))
		{
			expression = regex_replace(
				expression,
				pattern,
				std::to_string(simpleParse(regexResult.str())),
				std::regex_constants::format_first_only
			);
		}
	}

	return stod(expression);
}

extern "C" __declspec(dllexport) void __cdecl InitCalculator()
{
	calculord::calculator = new calculord::rxcalc();
}

extern "C" __declspec(dllexport) void __cdecl ReleaseCalculator()
{
	delete calculord::calculator;
}

extern "C" __declspec(dllexport) int __cdecl Calculate(const char* value, double& result)
{
	try
	{
		result = calculord::calculator->calculate(value);
		return 1;
	}
	catch (std::exception&)
	{
		return 0;
	}
}
