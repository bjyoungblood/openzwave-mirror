//-----------------------------------------------------------------------------
//
//	SensorMultilevel.h
//
//	Implementation of the Z-Wave COMMAND_CLASS_SENSOR_MULTILEVEL
//
//	Copyright (c) 2010 Mal Lansell <openzwave@lansell.org>
//
//	SOFTWARE NOTICE AND LICENSE
//
//	This file is part of OpenZWave.
//
//	OpenZWave is free software: you can redistribute it and/or modify
//	it under the terms of the GNU Lesser General Public License as published
//	by the Free Software Foundation, either version 3 of the License,
//	or (at your option) any later version.
//
//	OpenZWave is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//	GNU Lesser General Public License for more details.
//
//	You should have received a copy of the GNU Lesser General Public License
//	along with OpenZWave.  If not, see <http://www.gnu.org/licenses/>.
//
//-----------------------------------------------------------------------------

#ifndef _SensorMultilevel_H
#define _SensorMultilevel_H

#include "CommandClass.h"

namespace OpenZWave
{
	class ValueDecimal;

	/** \brief Implements COMMAND_CLASS_SENSOR_MULTILEVEL (0x31), a Z-Wave device command class.
	 */
	class SensorMultilevel: public CommandClass
	{
	public:
		static CommandClass* Create( uint32 const _homeId, uint8 const _nodeId ){ return new SensorMultilevel( _homeId, _nodeId ); }
		virtual ~SensorMultilevel(){}

		static uint8 const StaticGetCommandClassId(){ return 0x31; }
		static string const StaticGetCommandClassName(){ return "COMMAND_CLASS_SENSOR_MULTILEVEL"; }

		// From CommandClass
		virtual bool RequestState( uint32 const _requestFlags );
		virtual bool RequestValue( uint32 const _requestFlags, uint8 const _dummy = 0, uint8 const _instance = 0 );
		virtual uint8 const GetCommandClassId()const{ return StaticGetCommandClassId(); }
		virtual string const GetCommandClassName()const{ return StaticGetCommandClassName(); }
		virtual bool HandleMsg( uint8 const* _data, uint32 const _length, uint32 const _instance = 1 );
		
		// We return 1 instead of 3 because the latter versions only introduce new
		// sensor types, with no new message format changes.  Setting this to 1 prevents
		// OpenZWave from making an unecessary version requests. 
		virtual uint8 GetMaxVersion(){ return 1; }

	protected:
		virtual void CreateVars( uint8 const _instance );

	private:
		SensorMultilevel( uint32 const _homeId, uint8 const _nodeId ): CommandClass( _homeId, _nodeId ){}
	};

} // namespace OpenZWave


#endif

