#pragma once

#include <iostream>
#include <regex>
#include <exception>

namespace calculord
{
	class DivideByZeroExpression : public std::exception
	{
		const char* what() const throw() override
		{
			return "Delenie na nol' exception.";
		}
	} dnoe;

	class rxcalc
	{
		std::regex _simplePattern;

		std::vector<std::regex> _patterns;

		double simpleParse(std::string) const;
	public:

		rxcalc();

		double calculate(std::string) const;
	};

	rxcalc* calculator;
}

